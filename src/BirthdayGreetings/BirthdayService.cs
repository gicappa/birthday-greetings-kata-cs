using System.Net.Mail;

namespace BirthdayGreetings;

/// <summary>
/// Class <c>BirthdayService</c> contains the whole
/// business logic mixing several level of abstractions.
/// It open and read a file, parses its lines, select
/// employees with a birthday today and finally
/// send an email to them.
/// </summary>
public class BirthdayService
{
    private readonly IEmployeesRepo _employeesRepo;

    public BirthdayService(IEmployeesRepo employeesRepo)
    {
        _employeesRepo = employeesRepo;
    }

    public void SendGreetings(string fileName, XDate date, string smtpHost, int smtpPort)
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

    /// <summary>
    /// Sends a message to a certain user using a
    /// specific smtp server.
    /// </summary>
    /// <param name="smtpHost"></param>
    /// <param name="smtpPort"></param>
    /// <param name="from"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="recipient"></param>
    private static void SendMessage(string smtpHost, int smtpPort, string from, string subject, string body,
        string recipient)
    {
        using var client = new SmtpClient(smtpHost, smtpPort);
        var message = new MailMessage(from, recipient, subject, body);
        client.Send(message);
    }
}