using Logging;
using System;
using System.Linq;
using Xunit;

namespace CreditCardClearing.Tests;

public class AccountManagerTests
{
    private AccountManager _target = new AccountManager(new ConsoleLogger(LogLevel.Info));

    [Fact]
    public void ReadValidCsv_ValidData()
    {
        string[] lines = new[]
        {
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "11.11.2009;Mayer;6288081682946785;Fussl;558,88"
        };

        int expected = 2;
        int actual = _target.ReadCsv(lines, true);

        Assert.Equal(expected, actual);
        Assert.Equal(2, _target.NrAccounts);
        Assert.Single(_target.GetPayments("Hervis"));
        Assert.Single(_target.GetPayments("Fussl"));
        Assert.Empty(_target.GetPayments("PlusCity"));
    }

    [Fact]
    public void Read1Valid1InvalidCsv_ContinueOnErrorTrue_1ValidAccount()
    {
        string[] lines = new[]
        {
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "InvalidLine"
        };

        int expected = 1;
        int actual = _target.ReadCsv(lines, true);

        Assert.Equal(expected, actual);
        Assert.Equal(1, _target.NrAccounts);
        Assert.Single(_target.GetPayments("Hervis"));
    }

    [Fact]
    public void Read1Valid1InvalidCsv_ContinueOnErrorTrue_1ValidAccountAndLog()
    {
        var logger = new TestLogger();
        _target = new AccountManager(logger);

        string[] lines = new[]
        {
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "InvalidLine"
        };

        int expected = 1;
        int actual = _target.ReadCsv(lines, true);

        Assert.Equal(expected, actual);
        Assert.Equal(1, _target.NrAccounts);
        Assert.Single(logger.LoggedErrors);
    }

    [Fact]
    public void Read1Valid1InvalidCsv_ContinueOnErrorFalse_Throws()
    {
        var lines = new[]
        {
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "InvalidLine"
        };

        Assert.Throws<ArgumentException>(() => _target.ReadCsv(lines, false));
    }

    [Fact]
    public void ReadValidCsv_2PaymentsOn2Accounts_GetAccounts_2AccountsWithCorrectPayments()
    {
        string[] lines = new[]
        {
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "11.11.2009;Mayer;6288081682946785;Fussl;558,88"
        };

        _target.ReadCsv(lines, true);

        var accounts = _target.GetAccounts().ToList();
        Assert.Equal(2, accounts.Count);
        Assert.Single(accounts[0].Payments);
        Assert.Single(accounts[1].Payments);
    }

    [Fact]
    public void ReadValidCsv_3PaymentsOn3Accounts_GetAllPayments_PaymentsAreSortedCorrectly()
    {
        string[] lines = new[]
        {
            "11.11.2009;Mayer;6288081682946785;Fussl;558,88",
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "01.01.2010;Smith;1234567890123456;Amazon;100,00"
        };

        _target.ReadCsv(lines, true);

        var payments = _target.GetPayments("").ToList();
        Assert.Equal(3, payments.Count);
        Assert.True(payments[0].Date < payments[1].Date);
        Assert.True(payments[1].Date < payments[2].Date);
    }

    [Fact]
    public void ReadValidCsv_3PaymentsOn3Accounts_GetPayments_ByDate_PaymentsAreSortedCorrectly()
    {
        string[] lines = new[]
        {
            "11.11.2009;Mayer;6288081682946785;Fussl;558,88",
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "11.11.2009;Smith;1234567890123456;Amazon;100,00"
        };

        _target.ReadCsv(lines, true);

        var payments = _target.GetPayments(new DateTime(2009, 11, 11)).ToList();
        Assert.Equal(2, payments.Count);
        Assert.True(payments[0].Date == payments[1].Date);
    }

    [Fact]
    public void ReadValidCsv_3PaymentsOn3Accounts_GetPayments_ByPayee_PaymentsAreSortedCorrectly()
    {
        string[] lines = new[]
        {
            "11.11.2009;Mayer;6288081682946785;Fussl;558,88",
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "01.01.2010;Smith;1234567890123456;Amazon;100,00"
        };

        _target.ReadCsv(lines, true);

        var payments = _target.GetPayments("Fussl").ToList();
        Assert.Single(payments);
        Assert.Equal("Fussl", payments[0].Payee);
    }

    [Fact]
    public void ReadValidCsv_3PaymentsOn3Accounts_GetPayments_ByUnknownDate_ReturnsEmptyList()
    {
        string[] lines = new[]
        {
            "11.11.2009;Mayer;6288081682946785;Fussl;558,88",
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "01.01.2010;Smith;1234567890123456;Amazon;100,00"
        };

        _target.ReadCsv(lines, true);

        var payments = _target.GetPayments(new DateTime(2022, 1, 1)).ToList();
        Assert.Empty(payments);
    }

    [Fact]
    public void ReadValidCsv_3PaymentsOn3Accounts_GetPayments_ByUnknownPayee_ReturnsEmptyList()
    {
        string[] lines = new[]
        {
            "11.11.2009;Mayer;6288081682946785;Fussl;558,88",
            "09.06.2008;Michalik;1771914536488775;Hervis;817,32",
            "01.01.2010;Smith;1234567890123456;Amazon;100,00"
        };

        _target.ReadCsv(lines, true);

        var payments = _target.GetPayments("Unknown").ToList();
        Assert.Empty(payments);
    }
}

public class TestLogger : ILogger
{
    public List<string> LoggedErrors { get; } = new List<string>();

    public void LogError(string message)
    {
        LoggedErrors.Add(message);
    }

    public void LogInfo(string message) { }
}