using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace RealEstate.Utils
{
   static public class SendResetPasswordEmail
    {

        public static void SendEmail(string toEmail, string resetLink)
        {
            string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
            string Password = ConfigurationManager.AppSettings["EmailPassword"];
          
            string subject = "Đặt lại mật khẩu của bạn";
            string body = $@"
        <h2>Đặt lại mật khẩu</h2>
        <p>Nhấn vào liên kết bên dưới để đặt lại mật khẩu (hiệu lực trong 1 giờ):</p>
        <a href='{resetLink}'>Đặt lại mật khẩu</a>";

            using(MailMessage message = new MailMessage(fromEmail, toEmail))
            {
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                using(SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(fromEmail, Password);
                    smtp.Send(message);
                }
            }
        }
    }
}