using System.Net.Mail;
using NUnit.Framework;
using BirthdayGreetings;
using netDumbster.smtp;

namespace BirthdayGreetings.Tests;

[TestFixture]
public class AcceptanceTest
{
    private const string FilePath = "employee_data.txt";

    private SimpleSmtpServer? _smtpServer;
    private IEmployeesRepo _employeesRepo;
    private BirthdayService _birthdayService;
    private SmtpClient _smtpClient;
    private readonly XDate _xDate;

    public AcceptanceTest()
    {
        _xDate = new XDate("2008/10/08");
    }

    [SetUp]
    public void SetUp()
    {
        _smtpServer = SimpleSmtpServer.Start();
        _employeesRepo = new CsvEmployeesRepo(FilePath, new EmployeeFactory());
        _smtpClient = new SmtpClient("localhost", _smtpServer!.Configuration.Port);
        var greetingsFactory = new GreetingsFactory();


        var sendBirthdayGreetingsVisitor = new SendBirthdayGreetingsVisitor(greetingsFactory, _smtpClient, _xDate);
        
        _birthdayService = new BirthdayService(_employeesRepo, sendBirthdayGreetingsVisitor);
    }

    [TearDown]
    public void TearDown() => _smtpServer?.Stop();

    [Test]
    public void WillSendGreetingsWhenItsSomebodysBirthday()
    {
        _birthdayService.SendGreetings(_xDate);

        Assert.That(_smtpServer?.ReceivedEmailCount, Is.EqualTo(1), "message not sent?");
        var message = _smtpServer?.ReceivedEmail[0];
        Assert.That(message!.MessageParts[0].BodyData, Is.EqualTo("Happy Birthday, dear John!"));
        Assert.Multiple(() =>
        {
            Assert.That(message.Headers.Get("subject"), Is.EqualTo("Happy Birthday!"));
            Assert.That(message.ToAddresses, Has.Length.EqualTo(1));
        });
        Assert.That(message.ToAddresses[0].ToString(), Is.EqualTo("john.doe@foobar.com"));
    }

    [Test]
    public void WillNotSendEmailsWhenNobodysBirthday()
    {
        _birthdayService.SendGreetings(new XDate("2008/01/01"));
        Assert.That(_smtpServer?.ReceivedEmailCount, Is.EqualTo(0), "what? messages?");
    }
}