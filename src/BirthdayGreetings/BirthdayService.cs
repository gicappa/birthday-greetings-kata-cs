using System.Net.Mail;

namespace BirthdayGreetings;

internal class BirthdayService
{
    private readonly IEmployeesRepo _employeesRepo;

    internal BirthdayService(IEmployeesRepo employeesRepo)
    {
        _employeesRepo = employeesRepo;
    }

    internal void SendGreetings(string fileName, XDate date, string smtpHost, int smtpPort)
    {
        var employees = _employeesRepo.FindAllEmployees();
        foreach (var employee in employees)
        {
            if (employee.IsBirthday(date))
            {
                SendMessage(
                    smtpHost: smtpHost,
                    smtpPort: smtpPort,
                    from: "sender@here.com",
                    subject: "Happy Birthday!",
                    body: $"Happy Birthday, dear {employee.FirstName}!",
                    recipient: employee.Email);
            }
        }
    }

    private static void SendMessage(string smtpHost, int smtpPort, string from, string subject, string body,
        string recipient)
    {
        using var client = new SmtpClient(smtpHost, smtpPort);
        var message = new MailMessage(from, recipient, subject, body);
        client.Send(message);
    }
}