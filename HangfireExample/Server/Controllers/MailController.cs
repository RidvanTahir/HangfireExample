using Hangfire;
using HangfireExample.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HangfireExample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        // GET: api/<MailController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MailController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MailController>
        [HttpPost]
        public void Post([FromBody] string mail)
        {
            // Fire and Forget Job
            var jobId = BackgroundJob
                .Enqueue<IMailService>(mailService =>
                mailService.WelcomeMail(mail));
            Console.WriteLine(jobId);
        }

        // POST api/<MailController>
        [HttpPost("/newsletter")]
        public void NewsLetter([FromBody] string mail)
        {
            // Fire and Forget Job
            RecurringJob.AddOrUpdate<IMailService>(
                "newsletter",
                mailService => mailService.NewsLetter(mail),
                Cron.Minutely);
        }

        // PUT api/<MailController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string mail)
        {
            // Delayed Job
            var jobId = BackgroundJob
                .Schedule<IMailService>(mailService =>
                mailService.WelcomeMail(mail), TimeSpan.FromSeconds(15));
            Console.WriteLine(jobId);
        }

        // DELETE api/<MailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}