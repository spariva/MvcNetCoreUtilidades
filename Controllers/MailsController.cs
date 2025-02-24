using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace MvcNetCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        private IConfiguration configuration;

        public MailsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult SendMails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMails(string to, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");

            mail.From = new MailAddress(user);
            mail.To.Add(to);
            mail.Subject = asunto;
            mail.Body = mensaje;

            mail.IsBodyHtml = true; //Interpretar o no el html que haya en el mail
            mail.Priority = MailPriority.High; //Prioridad

            string host = this.configuration.GetValue<string>("MailSettings:Server:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Server:Port");
            bool ssl = this.configuration.GetValue<bool>("MailSettings:Server:Ssl");
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSettings:Server:DefaultCredentials");

            SmtpClient smtp = new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = ssl,
                UseDefaultCredentials = defaultCredentials
            };

            NetworkCredential credentials = new NetworkCredential(user, password);
            smtp.Credentials = credentials;
            await smtp.SendMailAsync(mail);
            ViewBag.Mensaje = "Mail enviado";

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
