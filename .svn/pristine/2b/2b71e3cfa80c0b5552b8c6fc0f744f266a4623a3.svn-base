using System.Text;
using System.Web.UI;
using System;
using System.Net.Mail;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;

namespace IAUHelper.Classes
{
	public static class EmailUtility
	{
		public static bool SendEmail(string fromEmail, string toEmail, string subject, string body)
		{
			Int32 Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
			string Host = ConfigurationManager.AppSettings["SMTPHost"].ToString(), NetworkUserName = ConfigurationManager.AppSettings["SMTPUser"].ToString(), NetworkPassword = ConfigurationManager.AppSettings["SMTPPassword"].ToString();
			SmtpClient client = new SmtpClient();
			client.Port = Port;
			client.Host = Host;
			if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSLS"].ToString()))
				client.EnableSsl = true;
			client.Timeout = 2000000;
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.UseDefaultCredentials = false;
			client.Credentials = new System.Net.NetworkCredential(NetworkUserName, NetworkPassword);
			MailMessage mm = new MailMessage(fromEmail, toEmail, subject, body);

			mm.BodyEncoding = UTF8Encoding.UTF8;
			mm.IsBodyHtml = true;
			mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
			try
			{
				client.Send(mm);
			}
			catch (Exception Err)
			{
				return false;
			}
			return true;
		}
	}
}
