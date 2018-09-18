using System.Net;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using CERPA.Models;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace CERPA
{
    public class Email
    {

        ApplicationUser user = GetUser();
        
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        public static string GetMailAddress()
        {
            ApplicationUser user = GetUser();
            return user.Email;
        }
        public static string GetPassword()
        {
            ApplicationUser user = GetUser();
            return user.PasswordHash;
        }
        public static ApplicationUser GetUser()
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());
            return user;
        }
        public static void SendEmail( string emailToAddress, string subject, string body)
        {
            string password = "Thedaddy1!";
            string emailFromAddress = GetMailAddress();
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("scotttesttracy@gmail.com");
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
       
        
    }
}