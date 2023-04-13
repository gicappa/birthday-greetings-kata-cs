using System.Net.Mail;

namespace BirthdayGreetings;

internal class BirthdayService
{
    private readonly IEmployeesRepo _employeesRepo;
    private readonly SmtpClient _smtpClient;
    private readonly IGreetingsFactory _greetingsFactory;

    internal BirthdayService(IEmployeesRepo employeesRepo, SmtpClient smtpClient, IGreetingsFactory greetingsFactory)
    {
        _employeesRepo = employeesRepo;
        _smtpClient = smtpClient;
        _greetingsFactory = greetingsFactory;
    }

    internal void SendGreetings(XDate date)
    {
        var employees = _employeesRepo.FindAllEmployees();
        foreach (var employee in employees)
        {
            if (employee.IsBirthday(date))
            {
                SendMessage(
                    from: "sender@here.com",
                    recipient: employee.Email,
                    greetings: _greetingsFactory.MakeFor(employee));
            }
        }
    }

    private void SendMessage(string from, string recipient, Greetings greetings)
    {
        var message = new MailMessage(from, recipient, greetings.Salutation, greetings.Message);
        
        _smtpClient.Send(message);
    }
}