using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace AWSUploadFile.Helper
{
    public class SendEmail
    {
        public void Send(string urlfile, string keyName)
        {

            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailFromName"]);
            var toAddress = new MailAddress("pashayev.ilkin@gmail.com", "");
            const string fromPassword = "ilk14789.";
            const string subject = "AWS S3 uploaded file";
            string body = "PreSignedURL <a href='" + urlfile + "'>" + keyName + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)


            };


            var message = new MailMessage(fromAddress, toAddress);

            message.Subject = subject;

            message.IsBodyHtml = true;
            message.Body = body;


            smtp.Send(message);

        }
    }
}