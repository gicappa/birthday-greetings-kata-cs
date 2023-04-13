namespace BirthdayGreetings;

internal interface IEmployeesRepo
{
    List<Employee> Employees { get; }
    void Load();
}