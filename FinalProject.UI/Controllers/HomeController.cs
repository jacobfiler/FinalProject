using FinalProject.UI.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace FinalProject.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact (ContactViewModel cvm)
        {
            //if it did not pass Model validation, return the view with the cvm object to repopulate form
            if (ModelState.IsValid)
            {
                string body = $"{cvm.Name} has sent you the following message: <br/ >{cvm.Message} <strong> from the email address:</strong> {cvm.Email}";
                //Message object
                MailMessage m = new MailMessage("no-reply@jacobfiler.com", "jacobwilliamfiler@gmail.com", cvm.Subject, body);

                //html formatting
                m.IsBodyHtml = true;

                m.Priority = MailPriority.High;

                m.ReplyToList.Add(cvm.Email);

                SmtpClient client = new SmtpClient("mail.jacobfiler.com");
                client.Credentials = new NetworkCredential("no-reply@jacobfiler.com", "Wobuai1!");

                try
                {
                    //send mail
                    client.Send(m);
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.StackTrace;
                }
                return View("EmailConfirmation");
            }

            return View(cvm);
        }
    }
}
