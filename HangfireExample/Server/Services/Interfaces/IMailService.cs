namespace HangfireExample.Server.Services.Interfaces
{
    public interface IMailService
    {
        void WelcomeMail(string mail);

        void UpdateMail(string mail);

        void NewsLetter(string mail);
    }
}