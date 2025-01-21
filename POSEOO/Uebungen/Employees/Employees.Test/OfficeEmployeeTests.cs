namespace Employees.Test;

public sealed class OfficeEmployeeTests
{
    [Fact]
    public void Construction_Simple()
    {
        const string Name = "Mia";
        const string Department = "Accounting";
        const Gender Gender = Gender.Female;
        const decimal Salary = 3211.99M;

        var employee = new OfficeEmployee(Name, Gender, Department, Salary);

        employee.Name.Should().Be(Name);
        employee.Department.Should().Be(Department);
        employee.Gender.Should().Be(Gender);
        employee.ToString().Should()
                .Be("My name is Mia, I identify as female and I work at the department Accounting as an employee");
        employee.Salary.Should().Be(Salary);
    }	
	
    [Theory]
    [InlineData("T", "too short, at least two characters")]
    [InlineData("lo", "has to start uppercase")]
    [InlineData("1g", "first character has to be a letter")]
    public void Construction_NameInvalid(string name, string reason)
    {
        var employee = new OfficeEmployee(name, Gender.Divers, "Canteen", 12.34M);

        employee.Name
                .Should()
                .NotBe(name)
                .And.Be("ERROR", reason);
    }

    [Fact]
    public void Construction_InvalidSalary()
    {
        var employee = new OfficeEmployee("Moritz", Gender.Male, "Sales", -123.45M);

        employee.Salary.Should().Be(0M, "invalid value, set to zero");
    }
}
