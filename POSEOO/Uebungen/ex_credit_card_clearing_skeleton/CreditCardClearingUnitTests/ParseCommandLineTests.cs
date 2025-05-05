using Logging;
using Program = ConsoleApp.Program;

namespace CreditCardClearing.Tests;

public class ParseCommandLineTests
{
    [Fact]
    public void ParseCommandLine_EmptyArgs_Throws()
    {
        string[] args = [];
        Assert.Throws<ArgumentException>(() =>
        {
            Program.ParseCommandLine(args, out bool continueOnError, out string paymentsFileName, out string logFileName);
        });
    }
    
    [Fact]
    public void ParseCommandLine_InvalidCE_Throws()
    {
        string[] args = [ "-ce", "invalid" ];
        Assert.Throws<ArgumentException>(() =>
        {
            Program.ParseCommandLine(args, out bool continueOnError, out string paymentsFileName, out string logFileName);
        });
    }
    
    [Fact]
    public void ParseCommandLine_ValidArgs_ValidResults()
    {
        string[] args = new[]
        {
            "-ce", "true", "-p", "test.csv", "-l", "test.log"
        };
        
        Program.ParseCommandLine(args, out bool continueOnError, out string paymentsFileName, out string logFileName);
        Assert.True(continueOnError);
        Assert.Equal("test.csv", paymentsFileName);
        Assert.Equal("test.log", logFileName);
    }

    [Fact]
    public void ParseCommandLIne_ContinueOnErrorFalse_ValidResults()
    {
        string[] args = new[]
        {
            "-ce", "false"
        };
        
        Program.ParseCommandLine(args, out bool continueOnError, out string paymentsFileName, out string logFileName);
        Assert.False(continueOnError);
        Assert.Equal("Payments.csv", paymentsFileName);
        Assert.StartsWith("log_", logFileName);
    }
    
   
}