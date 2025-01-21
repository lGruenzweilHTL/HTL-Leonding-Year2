namespace Employees;

/// <summary>
///     Represents an employee who works at an office
/// </summary>
/// <inheritdoc cref="Employee" />
public class OfficeEmployee : Employee
{
    /// <summary>
    ///     Creates a new employee who works at an office
    /// </summary>
    /// <param name="name">Name of the employee</param>
    /// <param name="gender">Gender of the employee</param>
    /// <param name="department">Department of the employee</param>
    /// <param name="monthlySalary">Monthly salary of the employee; cannot be negative</param>
    public OfficeEmployee(string name, Gender gender, string department,
                          decimal monthlySalary) : base(name, gender, department)
    {
        Salary = Math.Max(0, monthlySalary);
    }
    
    public override decimal Salary { get; }

    public override string ToString() => base.ToString() + " as an employee";
}
