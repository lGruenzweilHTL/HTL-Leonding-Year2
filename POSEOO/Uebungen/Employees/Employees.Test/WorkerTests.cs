namespace Employees.Test;

public sealed class WorkerTests
{
    [Fact]
    public void Construction_Minimal()
    {
        const string Name = "Sepp";
        const Gender Gender = Gender.Divers;
        const string Department = "Sawmill";
        
        var worker = new Worker(Name, Gender, Department);

        worker.Name.Should().Be(Name);
        worker.Gender.Should().Be(Gender);
        worker.Department.Should().Be(Department);
        worker.ToString()
              .Should().Be("My name is Sepp and I work at the department Sawmill as a worker");
        worker.Hours.Should().Be(default(double), "no value set yet");
        worker.HourlyWage.Should().Be(default(decimal), "no value set yet");
    }

    [Fact]
    public void Construction_Full()
    {
        const double Hours = 34.5D;
        const decimal HourlyWage = 112.68M;

        var worker = new Worker("Sepp", Gender.Divers, "Sawmill", Hours, HourlyWage);

        worker.ToString()
              .Should().Be("My name is Sepp and I work at the department Sawmill as a worker",
                           "not affected by hours or wage");
        worker.Hours.Should().Be(Hours);
        worker.HourlyWage.Should().Be(HourlyWage);
    }

    [Theory]
    [InlineData(12.34D, 12.34D, "valid value")]
    [InlineData(-12.34D, 0D, "invalid value, set to zero")]
    [InlineData(17 * 32D, Worker.MaxMonthlyWorkHours, "too big value, set to max")]
    public void Hours(double hours, double expectedHours, string reason)
    {
        var worker = new Worker("Susi", Gender.Divers, "Lasercutting",
                                hours, default(decimal));
        worker.Hours.Should().Be(expectedHours, reason);

        worker.Hours = hours;
        worker.Hours.Should().Be(expectedHours, reason);
    }

    [Theory]
    [MemberData(nameof(HourlyWageData))]
    public void HourlyWage(decimal hourlyWage, decimal expectedWage, string reason)
    {
        var worker = new Worker("Susi", Gender.Divers, "Lasercutting",
                                default(double), hourlyWage);
        worker.HourlyWage.Should().Be(expectedWage, reason);

        worker.HourlyWage = hourlyWage;
        worker.HourlyWage.Should().Be(expectedWage, reason);
    }

    [Theory]
    [MemberData(nameof(SalaryData))]
    public void Salary(double hours, decimal hourlyWage, decimal expectedSalary, string reason)
    {
        var worker = new Worker("Susi", Gender.Divers, "Lasercutting")
        {
            Hours = hours,
            HourlyWage = hourlyWage
        };

        worker.Salary.Should().Be(expectedSalary, reason);
    }

    [Fact]
    public void StringRepresentation()
    {
        var worker1 = new Worker("Sepp", Gender.Male, "Sawmill");
        var worker2 = new Worker("Susi", Gender.Female, "Welding");

        worker1.ToString()
               .Should().Be("My name is Sepp, I identify as male and I work at the department Sawmill as a worker");
        worker2.ToString()
               .Should().Be("My name is Susi, I identify as female and I work at the department Welding as a worker");
    }
    
    public static TheoryData<decimal, decimal, string> HourlyWageData =>
        new()
        {
            { 98.79M, 98.79M, "valid value" },
            { -19.99M, 0M, "invalid value, set to zero" }
        };

    public static TheoryData<double, decimal, decimal, string> SalaryData =>
        new()
        {
            { 8.5D * 20, 25.29M, 4299.30M, "valid values" },
            { -4D * 30, 10M, 0M, "invalid hours, set to zero so total salary is zero" },
            { 20D * 30, 10M, 4960M, "too many hours, set to max allowed" },
            { 160D, -12M, 0M, "invalid hourly wage, set to zero so total salary is zero" }
        };
}
