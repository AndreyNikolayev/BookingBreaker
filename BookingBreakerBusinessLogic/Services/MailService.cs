using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BookingBreakerBusinessLogic.Extensions;

namespace BookingBreakerBusinessLogic.Services
{
    public class MailService
    {

        private MailService()
        {
            MailLibrary.SMTP.NotDefaultSmtpSection = new System.Net.Configuration.SmtpSection();
            MailLibrary.SMTP.IsAuthenticate = true;
            MailLibrary.SMTP.EnableSsl = ConfigurationManager.AppSettings["EnableSsl"].ToBoolean();
            MailLibrary.SMTP.NotDefaultSmtpSection.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            MailLibrary.SMTP.NotDefaultSmtpSection.From = ConfigurationManager.AppSettings["SmtpUser"];
            MailLibrary.SMTP.NotDefaultSmtpSection.Network.DefaultCredentials = false;
            MailLibrary.SMTP.NotDefaultSmtpSection.Network.Host = ConfigurationManager.AppSettings["MailHost"];
            MailLibrary.SMTP.NotDefaultSmtpSection.Network.Port = ConfigurationManager.AppSettings["MailPort"].ToInteger();
            MailLibrary.SMTP.NotDefaultSmtpSection.Network.UserName = ConfigurationManager.AppSettings["SmtpUser"];
            MailLibrary.SMTP.NotDefaultSmtpSection.Network.Password = ConfigurationManager.AppSettings["SmtpPassword"];
        }

        private static MailService _defaultInstance;

        public static MailService DefaultInstance
        {
            get
            {
                if(_defaultInstance == null)
                {
                    _defaultInstance = new MailService();
                }

                return _defaultInstance;
            }
        }



        public bool SendEmail(string addressTo, string subject, string body)
        {
            var isSent = MailLibrary.SMTP.SendEmail(addressTo, subject, body,String.Empty, ConfigurationManager.AppSettings["SmtpUser"]);

            var errorMessage = MailLibrary.SMTP.ErrorMessage;

            return isSent;
        }
    }
}
