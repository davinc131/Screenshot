using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot.Common
{
    public class SendEmail
    {
        public static void Send()
        {
            try
            {
                var conf = SerializeConfiguration.ReaderXml();
                var directory = Directory.GetCurrentDirectory();
                var path = Path.Combine(directory, "Screenshot.png");

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(conf.Email);
                mail.To.Add(conf.Email);
                mail.Subject = "ScreenShot Teamviewer" + DateTime.Now.ToString();
                mail.Body = "ScreenShot Teamviewer";

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(path);
                mail.Attachments.Add(attachment);

                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(conf.Email, conf.Password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {

                throw new Exception("Send Email: " + ex.Message);
            }
        }
    }
}
