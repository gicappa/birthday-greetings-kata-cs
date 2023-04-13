using System.Net.Mail;

namespace BirthdayGreetings;

internal class BirthdayService
{
    private readonly IEmployeesRepo _employeesRepo;
    private readonly SmtpClient _smtpClient;

    internal BirthdayService(IEmployeesRepo employeesRepo, SmtpClient smtpClient)
    {
        _employeesRepo = employeesRepo;
        _smtpClient = smtpClient;
    }

    internal void SendGreetings(XDate date)
    {
        var employees = _employeesRepo.FindAllEmployees();
        foreach (var employee in employees)
        {
            if (employee.IsBirthday(date))
            {
                SendMessage(from: "sender@here.com",
                    subject: "Happy Birthday!",
                    body: $"Happy Birthday, dear {employee.FirstName}!",
                    recipient: employee.Email);
            }
        }
    }

    private void SendMessage(string from, string subject, string body,
        string recipient)
    {
        var message = new MailMessage(from, recipient, subject, body);
        _smtpClient.Send(message);
    }
}