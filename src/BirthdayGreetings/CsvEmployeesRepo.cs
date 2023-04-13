namespace BirthdayGreetings;

internal class CsvEmployeesRepo : IEmployeesRepo
{
    private readonly string _fileName;
    private readonly EmployeeFactory _employeeFactory;
    private readonly List<Employee> _employees = new();

    
    internal CsvEmployeesRepo(string fileName, EmployeeFactory employeeFactory)
    {
        _fileName = fileName;
        _employeeFactory = employeeFactory;
    }

    void IEmployeesRepo.Load()
    {
        using StreamReader input = new(_fileName);
        SkipHeader(input);
        
        ReadAllEmployeesFromCsv(input);
    }

    List<Employee> IEmployeesRepo.Employees => _employees;

    private void ReadAllEmployeesFromCsv(StreamReader input)
    {
        while (input.ReadLine() is { } str)
            _employees.Add(_employeeFactory.ParseEmployee(str));
    }

    private static void SkipHeader(TextReader input) => input.ReadLine();
}