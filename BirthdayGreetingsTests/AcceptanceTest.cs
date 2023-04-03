using NUnit.Framework;
using BirthdayGreetings;
using netDumbster.smtp;

namespace BirthdayGreetingsTests
{
	[TestFixture]
	public class AcceptanceTest
	{
		private BirthdayService? _service;
        private string? _filePath;

        private SimpleSmtpServer? _smtpServer;

		[SetUp]
		public void SetUp()
		{
			_smtpServer = SimpleSmtpServer.Start();
			_service = new BirthdayService();
            _filePath = Path.Combine(TestContext.CurrentContext.TestDirectory)
				+ "../../../../../BirthdayGreetings/employee_data.txt";
        }

		[TearDown]
		public void TearDown()
		{
            _smtpServer?.Stop();
		}

		[Test]
		public void WillSendGreetings_WhenItsSomebodysBirthday()
        {
            _service?.SendGreetings(_filePath!, new XDate("2008/10/08"), "localhost", _smtpServer.Configuration.Port);

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
		public void WillNotSendEmails_WhenNobodysBirthday()
		{
			_service?.SendGreetings(_filePath!, new XDate("2008/01/01"), "localhost", _smtpServer!.Configuration.Port);
			Assert.That(_smtpServer?.ReceivedEmailCount, Is.EqualTo(0), "what? messages?");
		}
	}
}
