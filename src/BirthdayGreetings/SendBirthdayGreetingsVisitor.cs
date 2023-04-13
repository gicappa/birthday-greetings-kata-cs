using System.Net.Mail;

namespace BirthdayGreetings;

class SendBirthdayGreetingsVisitor : IVisitor<Employee>
{
    internal XDate Today { get; set; }
    private readonly IGreetingsFactory _greetingsFactory;
    private readonly SmtpClient _smtpClient;

    internal SendBirthdayGreetingsVisitor(IGreetingsFactory greetingsFactory, SmtpClient smtpClient, XDate today)
    {
        _greetingsFactory = greetingsFactory;
        _smtpClient = smtpClient;
        Today = today;
    }

    void IVisitor<Employee>.Visit(Employee employee)
    {
        if (employee.IsBirthday(Today))
        {
            SendMessage(
                from: "sender@here.com",
                recipient: employee.Email,
                greetings: _greetingsFactory.MakeFor(employee));
        }
    }

    private void SendMessage(string from, string recipient, Greetings greetings)
    {
        var message = new MailMessage(from, recipient, greetings.Salutation, greetings.Message);

        _smtpClient.Send(message);
    }
}