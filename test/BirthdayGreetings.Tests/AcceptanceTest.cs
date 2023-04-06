using NUnit.Framework;
using BirthdayGreetings;
using netDumbster.smtp;

namespace BirthdayGreetings.Tests;

[TestFixture]
public class AcceptanceTest
{
  private const string FilePath = "employee_data.txt";

  private SimpleSmtpServer? _smtpServer;

  [SetUp]
  public void SetUp()
  {
    _smtpServer = SimpleSmtpServer.Start();
  }

  [TearDown]
  public void TearDown() => _smtpServer?.Stop();

  [Test]
  public void WillSendGreetingsWhenItsSomebodysBirthday()
  {
    BirthdayService.SendGreetings(FilePath!, new XDate("2008/10/08"), "localhost", _smtpServer!.Configuration.Port);

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
    BirthdayService.SendGreetings(FilePath!, new XDate("2008/01/01"), "localhost", _smtpServer!.Configuration.Port);
    Assert.That(_smtpServer?.ReceivedEmailCount, Is.EqualTo(0), "what? messages?");
  }
}