using AWSUploadFile.Services;
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
    public class EmailService
    {
        private SmtpClient smtp { get; set; }
        private MailAddress fromAddress { get; set; }
        private string fromPassword { get; set; }
        private string subject { get; set; }
        private string body { get; set; }

        public EmailService()
        {

        }

        public void PrepareEmail(string preSignedURL, string keyName)
        {
            this.subject = "AWS uploaded file";
            this.body = $"PreSignedURL <a href='{preSignedURL}'>{keyName}</a>";
            this.fromAddress = new MailAddress(ConfigurationService.Instance.EmailFrom,
                                                ConfigurationService.Instance.EmailFromName);
            this.fromPassword = ConfigurationService.Instance.EmailPassword;
            string emailNetworkValue = ConfigurationService.Instance.EmailNetwork;
            SmtpDeliveryMethod networkValue = (SmtpDeliveryMethod)Enum.Parse(typeof(SmtpDeliveryMethod), emailNetworkValue);
            this.smtp = new SmtpClient
            {
                Host = ConfigurationService.Instance.EmailHost,
                Port = Int32.Parse(ConfigurationService.Instance.EmailPort),
                EnableSsl = Convert.ToBoolean(ConfigurationService.Instance.EmailEnableSsl),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = Convert.ToBoolean(ConfigurationService.Instance.EmailUseDefaultCredentials),
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