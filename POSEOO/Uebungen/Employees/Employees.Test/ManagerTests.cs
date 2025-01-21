namespace Employees.Test;

public sealed class ManagerTests
{
    [Fact]
    public void Construction()
    {
        const string Name = "Lisa";
        const string Department = "Logistics";
        const Gender Gender = Gender.Female;
        const decimal BaseSalary = 4500.89M;
        
        var manager = new Manager(Name, Gender, Department, BaseSalary);

        manager.Name.Should().Be(Name);
        manager.Department.Should().Be(Department);
        manager.Gender.Should().Be(Gender);
        manager.Salary.Should().Be(5401.068M, "managers earn 20% more");
        manager.ToString().Should().Be("My name is Lisa and I'm head of the Logistics department");
    }
}
