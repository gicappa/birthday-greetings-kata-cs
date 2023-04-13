namespace BirthdayGreetings;

internal class CsvEmployeesRepo : IEmployeesRepo
{
    private readonly string _fileName;

    internal CsvEmployeesRepo(string fileName)
    {
        _fileName = fileName;
    }

    List<Employee> IEmployeesRepo.FindAllEmployees()
    {
        using StreamReader input = new(_fileName);
        SkipHeader(input);
        return ReadAllEmployeesFromCsv(input);
    }

    private static List<Employee> ReadAllEmployeesFromCsv(StreamReader input)
    {
        var employees = new List<Employee>();

        while (input.ReadLine() is { } str)
            employees.Add(ParseEmployee(str));

        return employees;
    }

    private static void SkipHeader(TextReader input) => input.ReadLine();

    private static Employee ParseEmployee(string str)
    {
        var employeeData = str.Split(new char[] { ',' }, 1000);

        Employee employee = new(
            employeeData[1].Trim(),
            employeeData[0].Trim(),
            employeeData[2].Trim(),
            employeeData[3].Trim());

        return employee;
    }
}