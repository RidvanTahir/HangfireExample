using HangfireExample.Server.Services.Interfaces;

namespace HangfireExample.Server.Services
{
    public class MailService : IMailService
    {
        public void WelcomeMail(string mail)
        {
            Console.WriteLine(mail);
        }

        public void UpdateMail(string mail)
        {
            Console.WriteLine(mail);
        }

        public void NewsLetter(string mail)
        {
            Console.WriteLine(mail);
        }
    }
}