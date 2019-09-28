using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTV48Schmalkalden.Models;

namespace TTV48Schmalkalden.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(ContactViewModel contactModel)
        {
            if (ModelState.IsValid)
            {
                SmtpClient client = new SmtpClient("ttv48schmalkalden.com");
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("info@ttv48schmalkalden.com", "Su6(f$U7DQT$");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("info@ttv48schmalkalden.com");
                mailMessage.To.Add(contactModel.Email);
                mailMessage.Body = contactModel.Message;
                mailMessage.Subject = contactModel.Subject;
                client.Send(mailMessage);
                return RedirectToAction("");
            }

            return View(contactModel);
        }

        public IActionResult Redirection(string tag)
        {
            return Redirect(Url.RouteUrl(new { controller = "Contact", action = "Index" }) + "#" + tag);
        }
    }
}