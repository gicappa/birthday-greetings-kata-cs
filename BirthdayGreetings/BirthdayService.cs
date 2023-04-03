using System;
using System.Net.Mail;

namespace BirthdayGreetings
{
	public class BirthdayService
	{
		public void SendGreetings(String fileName, XDate date, String smtpHost, int smtpPort) 
		{
			StreamReader input = new(fileName);
			String str = "";
			str = input.ReadLine(); // skip header
			while ((str = input.ReadLine()) != null) {
				String[] employeeData = str.Split(new char[] {','}, 1000);
				Employee employee = new(employeeData[1].Trim(), employeeData[0].Trim(), employeeData[2].Trim(), employeeData[3].Trim());
				if (employee.IsBirthday(date)) {
					String recipient = employee.GetEmail();
					String body = "Happy Birthday, dear %NAME%".Replace("%NAME%", employee.GetFirstName());
                    const String subject = "Happy Birthday!";
                    SendMessage(smtpHost, smtpPort, "sender@here.com", subject, body, recipient);
				}
			}
		}

		private void SendMessage(string smtpHost, int smtpPort, string from, string subject, string body, string recipient)
		{
			var client = new SmtpClient(smtpHost, smtpPort);
			var message = new MailMessage (from, recipient, subject, body);
			client.Send (message);
			client.Dispose ();
		}

	}
}

