namespace BirthdayGreetings;

internal class BirthdayService
{
    private readonly IEmployeesRepo _employeesRepo;
    private readonly IVisitor<Employee> _visitor;

    internal BirthdayService(IEmployeesRepo employeesRepo, IVisitor<Employee> visitor)
    {
        _employeesRepo = employeesRepo;
        _visitor = visitor;
    }

    internal void SendGreetings(XDate date)
    {
        ((SendBirthdayGreetingsVisitor)_visitor).Today = date;
        _employeesRepo.Load();

        for (var index = 0; index < _employeesRepo.Employees.Count; index++)
        {
            var employee = _employeesRepo.Employees[index];
            ((IElement<Employee>)employee).Accept(_visitor);
        }
    }
}