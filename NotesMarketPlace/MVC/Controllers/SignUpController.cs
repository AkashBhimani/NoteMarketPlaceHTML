using NotesMarketPlace.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    public class SignUpController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        [Route("SignUp")]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(SignUpViewModel user)
        {
            if (ModelState.IsValid)
            {
                Users obj = new Users()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailID = user.EmailID,
                    Password = user.Password,
                    RoleID = 3,
                };

                obj.Password = Encrypt(obj.Password);
                ModelState.Clear();
                context.Users.Add(obj);
                context.SaveChanges();
                TempData["Success"] = "Your account is successfully created.";
                TempData["UserName"] = user.FirstName;
                SendVerificationLinkEmail(obj.EmailID);
            }
            return View("SignUp");
        }

        public ActionResult EmailVerificationModal(string emailID)
        {
            var user = context.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
            TempData["UserName"] = user.FirstName;
            return View(user);
        }

        public void SendVerificationLinkEmail(string emailID)
        {
            var user = context.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
            var verifyUrl = "/SignUp/EmailVerificationModal?emailID=" + user.EmailID;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("akash.bhimani.046@gmail.com", "Akash Bhimani");
            var toEmail = new MailAddress(user.EmailID);
            var fromEmailPassword = "Akash@046"; // Replace with actual password
            string subject = "Note Marketplace - Email Verification";

            string body = "<br /> Hello " + user.FirstName + ", <br /> Thank you for signing up with us." +
                " Please click on below link to verify your email address and to do login" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> " +
                " <br /><br /> Rewards, <br /> Notes Marketplace";

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
        }

        [HttpGet]
        public ActionResult VerifyAccount(string emailid)
        {
            var user = context.Users.Where(a => a.EmailID == emailid).FirstOrDefault();
            if (user != null)
            {
                user.IsEmailVerified = true;
                user.IsActive = true;
                context.SaveChanges();
            }
            return RedirectToAction("Login", "Login");
        }

        public string Encrypt(string str)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

    }
}