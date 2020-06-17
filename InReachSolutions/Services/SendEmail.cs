using InReachSolutions.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;

namespace InReachSolutions.Helper
{
    public class SendEmail
    {
        private SmtpClient smtp { get; set; }
        private MailAddress fromAddress { get; set; }
        private string fromPassword { get; set; }
        private string subject { get; set; }
        private string body { get; set; }
        public SendEmail(string preSignedURL, string keyName)
        {
            this.subject = "AWS uploaded file";
            this.body = $"PreSignedURL <a href='{preSignedURL}'>{keyName}</a>";
            this.fromAddress = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailFromName"]);
            this.fromPassword = ConfigurationManager.AppSettings["EmailPassword"];
            string configValue = ConfigurationManager.AppSettings["EmailNetwork"];
            SmtpDeliveryMethod networkValue = (SmtpDeliveryMethod)Enum.Parse(typeof(SmtpDeliveryMethod), configValue);

            this.smtp = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["EmailHost"],
                Port = Int32.Parse(ConfigurationManager.AppSettings["EmailPort"]),
                EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailEnableSsl"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,//networkValue,
                UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailUseDefaultCredentials"]),
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
        }
        public void Send(string to)
        {
            var toAddress = new MailAddress(to, "");
            string Emailsubject = subject;
            string Emailbody = this.body;
            var message = new MailMessage(this.fromAddress, toAddress);
            message.Subject = this.subject;
            message.IsBodyHtml = true;
            message.Body = body;
            try
            {
                smtp.Send(message);
            }
            catch(Exception ex)
            {
                throw new SendEmailException($"Couldn't send email. {ex.Message}");
            }
        }
    }
}