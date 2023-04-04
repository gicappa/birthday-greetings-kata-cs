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
  public static void SendGreetings(string fileName, XDate date, string smtpHost, int smtpPort)
  {
    StreamReader input = new(fileName);
    var str = "";
    str = input.ReadLine(); // skip header
    while ((str = input.ReadLine()) != null)
    {
      var employeeData = str.Split(new char[] { ',' }, 1000);
      Employee employee = new(employeeData[1].Trim(), employeeData[0].Trim(), employeeData[2].Trim(),
        employeeData[3].Trim());
      if (employee.IsBirthday(date))
      {
        SendMessage(
          smtpHost: smtpHost,
          smtpPort: smtpPort,
          from: "sender@here.com",
          subject: "Happy Birthday!",
          body: $"Happy Birthday, dear {employee.FirstName}",
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