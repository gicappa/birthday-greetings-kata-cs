namespace BirthdayGreetings;

internal class EmployeeFactory
{
    internal Employee ParseEmployee(string row)
    {
        var employeeData = row.Split(new[] { ',' }, 1000);

        Employee employee = new(
            employeeData[1].Trim(),
            employeeData[0].Trim(),
            employeeData[2].Trim(),
            employeeData[3].Trim());

        return employee;
    }
}