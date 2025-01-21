namespace Employees;

/// <summary>
///     Represents an employee who performs physical labor
/// </summary>
public sealed class Worker : Employee
{
    /// <summary>
    ///     The absolute maximum of hours worked per month
    /// </summary>
    public const int MaxMonthlyWorkHours = 16 * 31;

    private decimal _hourlyWage;
    private double _hours;

    /// <summary>
    ///     Creates a new worker.
    ///     Working hours and hourly wage has to be set additionally for meaningful function.
    /// </summary>
    /// <param name="name">Name of the worker</param>
    /// <param name="gender">Gender of the worker</param>
    /// <param name="department">Department the worker works at</param>
    public Worker(string name, Gender gender, string department)
        : base(name, gender, department) { }

    /// <summary>
    ///     Creates a new worker
    /// </summary>
    /// <param name="name">Name of the worker</param>
    /// <param name="gender">Gender of the worker</param>
    /// <param name="department">Department the worker works at</param>
    /// <param name="hours">Hours worked per month</param>
    /// <param name="hourlyWage">Wage per hour worked</param>
    public Worker(string name, Gender gender, string department, double hours, decimal hourlyWage)
        : this(name, gender, department)
    {
        Hours = hours;
        HourlyWage = hourlyWage;
    }

    /// <summary>
    ///     Gets or sets the monthly worked hours.
    ///     Cannot be negative or exceed <see cref="MaxMonthlyWorkHours" />.
    /// </summary>
    public double Hours
    {
        get => _hours;
        set => _hours = Math.Clamp(value, 0, MaxMonthlyWorkHours);
    }

    /// <summary>
    ///     Gets or sets the wage per hour worked.
    ///     Cannot be negative.
    /// </summary>
    public decimal HourlyWage
    {
        get => _hourlyWage;
        set => _hourlyWage = Math.Max(value, 0);
    }


    public override decimal Salary => (decimal)Hours * HourlyWage;
    public override string ToString() => base.ToString() + " as a worker";
}
