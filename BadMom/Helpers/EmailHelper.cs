using BadMom.Models.Registration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BadMom.Helpers
{
    public class EmailHelper
    {

        public bool SendRegistrationMessage(string login, string passHash, string email, EmailType type)
        {
            SmtpClient client = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("info@goodmomonline.com.ua");
            mailMessage.To.Add(email);
            mailMessage.IsBodyHtml = true;
            switch (type)
            {
                case EmailType.Registration:
                    mailMessage.Subject = "Подтвердить регистрацию на GoodMom";
                    mailMessage.Body = GetBody(WebConfigurationManager.AppSettings["AuthTemplate"].ToString(), login, passHash, WebConfigurationManager.AppSettings["AuthLink"].ToString());
                    break;
                case EmailType.ChangePassword:
                    mailMessage.Subject = "Подтвердить смену пароля на GoodMom";
                    mailMessage.Body = GetBody(WebConfigurationManager.AppSettings["ChangePassTemplate"].ToString(), login, passHash, WebConfigurationManager.AppSettings["ChangePassLink"].ToString());
                    break;
                default:
                    break;
            }

            client.Send(mailMessage);
            return true;
        }

        private string GetBody(string templateName, string login, string password, string link)
        {
            string body;
            using (var sr = new StreamReader(HttpContext.Current.Server.MapPath("\\App_Data\\Template\\") + templateName))
            {
                body = sr.ReadToEnd();
            }

            string messageBody = string.Format(body, login, link + "?login=" + login + "&pass=" + password);
            return messageBody;
        }

        public enum EmailType
        {
            Registration,
            ChangePassword
        }

        public bool SendMail(string subject, string body, string email)
        {
            SmtpClient client = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("info@goodmomonline.com.ua");
            mailMessage.To.Add(email);
            mailMessage.IsBodyHtml = true;

            mailMessage.Subject = subject;
            mailMessage.Body = body;


            client.Send(mailMessage);
            return true;
        }

    }
}