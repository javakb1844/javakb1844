using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Library.Core.Services;
using VM.HRMS;

namespace Services.HRMS
{
    public class EmailServices
    {
        private static string Mailaddress
        {
            get
            {
                return ConfigurationManager.AppSettings["MailAddress"];
            }
        }
        private static string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["Password"];
            }
        }
        private static string Port
        {
            get
            {
                return ConfigurationManager.AppSettings["Port"];
            }
        }
        private static string Host
        {
            get
            {
                return ConfigurationManager.AppSettings["Host"];
            }
        }
        public static async Task<Result<bool>> SendMail(EmailViewModel email)
        {
            var result = new Result<bool>();
            try
            {

                var mail = new MailMessage(
                                from: Mailaddress,
                                to: email.Recipient,
                                subject: email.Subject,
                                body: string.Format(email.BodyMessage));
                mail.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = Mailaddress,  //From Web Config
                        Password = Password      //From Web Config 
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Host;
                    smtp.Port = Convert.ToInt32(Port);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
                result.ResultType = ResultType.Success;
                result.Data = true;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
    }
}
