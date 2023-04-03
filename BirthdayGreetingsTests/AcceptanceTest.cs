using NUnit.Framework;
using BirthdayGreetings;
using netDumbster.smtp;

namespace BirthdayGreetingsTests
{
	[TestFixture]
	public class AcceptanceTest
	{
		private readonly BirthdayService service = new();
		private readonly string FILE_NAME = "../../../BirthdayGreetings/employee_data.txt";
		private readonly SimpleSmtpServer smtpServer = SimpleSmtpServer.Start();

		// [SetUp]
		// public void SetUp()
		// {
		// 	service = new BirthdayService();
		// }

		[TearDown]
		public void TearDown()
		{
            smtpServer.Stop();
		}

		[Test]
		public void WillSendGreetings_WhenItsSomebodysBirthday()
        {
            service?.SendGreetings(FILE_NAME, new XDate("2008/10/08"), "localhost", smtpServer.Configuration.Port);

			Assert.That(smtpServer?.ReceivedEmailCount, Is.EqualTo(1), "message not sent?");
			var message = smtpServer?.ReceivedEmail [0];
			Assert.That(message.MessageParts[0].BodyData, Is.EqualTo("Happy Birthday, dear John!"));
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
			service?.SendGreetings(FILE_NAME, new XDate("2008/01/01"), "localhost", smtpServer.Configuration.Port);
			Assert.That(smtpServer?.ReceivedEmailCount, Is.EqualTo(0), "what? messages?");
		}
	}
}
