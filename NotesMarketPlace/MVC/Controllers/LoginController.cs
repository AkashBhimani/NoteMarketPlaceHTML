using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using NotesMarketPlace.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace NotesMarketPlace.Controllers
{
    public class LoginController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyUser(LoginViewModel user)
        {
            ViewBag.IsValid = true;
            var db_user = context.Users.Where(m=>m.EmailID == user.EmailId).FirstOrDefault();

            if(db_user == null)
            {
                ViewBag.IsValid = false;
                ModelState.AddModelError("", "This account dosen't exist.");
            }
            if (db_user.IsActive == false)
            {
                ViewBag.IsValid = false;
                ModelState.AddModelError("", "You are inactive.");
            }
            else if (db_user != null && db_user.IsEmailVerified == false)
            {
                ViewBag.IsValid = false;
                ModelState.AddModelError("", "Please first goto Email and Verify it.");
            }
            else if (db_user != null && db_user.Password != Encrypt(user.Password))
            {
                ViewBag.IsValid = false;
                ModelState.AddModelError("", "Please enter correct password.");
            }
            else if (db_user != null && db_user.Password == Encrypt(user.Password))
            {
                FormsAuthentication.SetAuthCookie(db_user.EmailID, true);
                if (db_user.RoleID == 3)
                {
                    if (context.UserProfile.Where(x => x.UserID == db_user.ID).Any())
                    {
                        return RedirectToAction("NoteList", "SearchNotes");
                    }
                    else
                    {
                        return RedirectToAction("UserProfile", "UserAccount");
                    }
                }
                else
                {
                    return RedirectToAction("Dashboard", "Dashboard", new { area = "Admin" });
                }
                
            }

            return View("Login");

        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyEmailAddress(UsersViewModel model)
        {
            var db_user = context.Users.Where(m => m.EmailID == model.EmailID).FirstOrDefault();

            if(db_user != null)
            {
                SendRandomPasswordEmail(model.EmailID);
                TempData["Registered"] = " Your password has been changed successfully and newly generated password is sent on your registered email address.";
                return RedirectToAction("Login","Login");
            }
            else
            {
                TempData["NotRegistered"] = "Please enter correct Email address.";
                ModelState.Clear();
                return View("ForgotPassword");
            }
        }

        public void SendRandomPasswordEmail(string emailID)
        {
            string password = GeneratePassword();
            var user = context.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
            string supportEmailID = context.ManageSystemConfiguration.Select(x => x.SupportEmail).FirstOrDefault();
            user.Password = Encrypt(password);
            context.SaveChanges();
            var fromEmail = new MailAddress(supportEmailID);
            var toEmail = new MailAddress(user.EmailID);
            var fromEmailPassword = "******";
            string subject = "New Temporary Password has been created for you";

            string body = "<br /> Hello, <br /><br /> We have generated a new password for you " +
                " <br/> Password: " + password  +
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

        public string GeneratePassword()
        {
            string PasswordLength = "4";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0,@,$,!,%,*,#,?,&";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;
            }
            return NewPassword;
        }
    }
}