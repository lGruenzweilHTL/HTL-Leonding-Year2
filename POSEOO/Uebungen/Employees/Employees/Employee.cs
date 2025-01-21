namespace Employees;

/// <summary>
///     Represents an employee of a company
/// </summary>
public abstract class Employee
{
    private readonly string _name = default!;

    /// <summary>
    ///     Creates a new employee
    /// </summary>
    /// <param name="name">Name of the employee</param>
    /// <param name="gender">Gender of the employee</param>
    /// <param name="department">Department the employee works at</param>
    protected Employee(string name, Gender gender, string department)
    {
        Name = name;
        Gender = gender;
        Department = department;
    }

    /// <summary>
    ///     Gets the name of the employee
    /// </summary>
    public string Name
    {
        get => _name;
        private init
        {
            if (value.Length < 2 || !char.IsAsciiLetterUpper(value[0])) _name = "ERROR";
            else _name = value;
        }
    }

    /// <summary>
    ///     Gets the department of the employee
    /// </summary>
    public string Department { get; }

    /// <summary>
    ///     Gets the gender of the employee
    /// </summary>
    public Gender Gender { get; }

    /// <summary>
    ///     Gets the salary of the employee
    /// </summary>
    public abstract decimal Salary { get; }

    /// <summary>
    ///     Creates a string representation of the employee with some basic information
    /// </summary>
    /// <returns>String representation of the employee</returns>
    public override string ToString()
    {
        string genderString = Gender switch
        {
            Gender.Female => ", I identify as female",
            Gender.Male => ", I identify as male",
            _ => ""
        };
        return $"My name is {_name}{genderString} and I work at the department {Department}";
    }
}

/// <summary>
///     Represents the possible genders for an employee
/// </summary>
public enum Gender
{
    Male,
    Female,
    Divers
}
