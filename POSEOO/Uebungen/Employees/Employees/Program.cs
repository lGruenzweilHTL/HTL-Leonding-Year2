using System.Text;
using Employees;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("*** Employees ***");

const string Department = "Development";
var employee1 = new Manager("Hermann", Gender.Male, Department, 3800M);
var employee2 = new OfficeEmployee("Maria", Gender.Female, Department, 3800M);
var employee3 = new Worker("Alex", Gender.Divers, Department, 38.5D * 4, 18.40M);

var employees = new List<Employee>
{
    employee1, employee2, employee3
};
foreach (var employee in employees)
{
    Console.WriteLine(employee);
}