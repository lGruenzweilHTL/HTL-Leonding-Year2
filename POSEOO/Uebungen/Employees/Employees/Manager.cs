namespace Employees;

/// <summary>
///     Represents an employee who is manager of a department
/// </summary>
/// <inheritdoc cref="OfficeEmployee" />
public sealed class Manager : OfficeEmployee
{
    /// <summary>
    ///     Creates a new manager
    /// </summary>
    /// <param name="name">Name of the manager</param>
    /// <param name="gender">Gender of the manager</param>
    /// <param name="department">Department of the manager</param>
    /// <param name="monthlySalary">Monthly base salary of the manager</param>
    public Manager(string name, Gender gender, string department,
                   decimal monthlySalary) : base(name, gender, department, monthlySalary) { }

    /// <summary>
    ///     Gets the actual salary for the manger, which is higher than the base salary
    /// </summary>
    public override decimal Salary => base.Salary * 1.2m;

    public override string ToString() => $"My name is {Name} and I'm head of the {Department} department";
}
