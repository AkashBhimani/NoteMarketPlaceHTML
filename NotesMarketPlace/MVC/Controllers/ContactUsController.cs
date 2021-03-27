using NotesMarketPlace.Models;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    public class ContactUsController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            if (User.Identity.IsAuthenticated)
            {
                var emailId = User.Identity.Name.ToString();
                var loggedInUser = context.Users.Where(x => x.EmailID == emailId).Select(x =>
                new ContactUsViewModel
                {
                    FullName = x.FirstName + " " + x.LastName,
                    EmailID = x.EmailID
                }).FirstOrDefault();

                return View(loggedInUser);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult SendContactUsEmail(ContactUsViewModel model)
        {
            string emailId = User.Identity.Name;
            var fromEmail = new MailAddress("akash.bhimani.046@gmail.com", "Akash Bhimani");
            var toEmail = new MailAddress(emailId);
            var fromEmailPassword = "Akash@046"; // Replace with actual password
            string subject = model.FullName + " - Query";

            string body = "<br /> Hello, <br />" +
                "<br />" + model.Comments_Questions + "<br />" +
                " <br /><br /> Rewards, <br />" + model.FullName;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

            smtp.Send(message);
            ModelState.Clear();

            return View("ContactUs");
        }
    }
}