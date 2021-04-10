using NotesMarketPlace.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using NotesMarketPlace.Areas.Admin.Data;
using System.Data.Entity;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        public ActionResult MyProfile(int id)
        {
            var countries = context.Countries.Distinct().ToList();

            var secondaryEmail = context.UserProfile.Where(x => x.UserID == id).Select(x => x.SecondaryEmail).FirstOrDefault();
            var model = context.Users.Where(x => x.ID == id).Select(x =>
                new MyProfileViewModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailID = x.EmailID,
                    SecondaryEmail = secondaryEmail,
                    PhoneNumber_CountryCode = x.PhoneNumber_CountryCode,
                    PhoneNumber = x.PhoneNumber
                }).FirstOrDefault();

            model.Countries = countries;

            return View(model);
        }

        [HttpPost]
        public ActionResult MyProfile(MyProfileViewModel model)
        {
            var emailId = User.Identity.Name.ToString();
            var loggedInUser = context.Users.Where(m => m.EmailID == emailId).FirstOrDefault();

            Users user = context.Users.Where(x => x.ID == model.ID).FirstOrDefault();
            UserProfile myprofile = context.UserProfile.Where(x => x.UserID == model.ID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                user.PhoneNumber = model.PhoneNumber;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = loggedInUser.ID;

                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();

                string _FilePath = "";

                //For Profile Picture
                if (model.DefaultProfileImage != null && model.DefaultProfileImage.ContentLength > 0)
                {
                    //For create Directory
                    _FilePath = Path.Combine(Server.MapPath("~/Members"), user.ID.ToString());

                    //Check Wheter subdirectory is already exists or not
                    if (!Directory.Exists(_FilePath))
                    {
                        Directory.CreateDirectory(_FilePath);
                    }

                    string _FileName = Path.GetFileNameWithoutExtension(model.DefaultProfileImage.FileName);
                    string extension = Path.GetExtension(model.DefaultProfileImage.FileName);

                    _FileName = "DP_" + System.DateTime.Now.ToString("dd-MM-yyyy") + extension;

                    _FilePath = Path.Combine(Server.MapPath("~/Members/" + user.ID), _FileName);
                    model.DefaultProfileImage.SaveAs(_FilePath);
                    
                    _FilePath = Path.Combine(("/Members/" + user.ID), _FileName);
                }

                if (myprofile != null)
                {
                    myprofile.UserID = user.ID;
                    myprofile.ProfilePicture = _FilePath;
                    myprofile.SecondaryEmail = model.SecondaryEmail;
                    myprofile.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    myprofile.PhoneNumber = model.PhoneNumber;
                    myprofile.ModifieDate = DateTime.Now;
                    myprofile.ModifiedBy = loggedInUser.ID;

                    context.Entry(myprofile).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    myprofile = new UserProfile();

                    myprofile.UserID = user.ID;
                    myprofile.ProfilePicture = _FilePath;
                    myprofile.SecondaryEmail = model.SecondaryEmail;
                    myprofile.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    myprofile.PhoneNumber = model.PhoneNumber;
                    myprofile.SubmittedDate = DateTime.Now;
                    myprofile.SubmittedBy = loggedInUser.ID;

                    context.UserProfile.Add(myprofile);
                    context.SaveChanges();
                }
            }

            ModelState.Clear();
            MyProfileViewModel obj = new MyProfileViewModel();
            obj.Countries = context.Countries.Distinct().ToList();

            return View(obj);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddChangedPassword(ChangePasswordViewModel model)
        {
            var emailId = User.Identity.Name.ToString();
            var user = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            if (Encrypt(model.OldPassword) != user.Password)
            {
                TempData["IncorrectPwd"] = "Your old password is incorrect.";

                return RedirectToAction("ChangePassword");
            }
            else
            {
                TempData["Success"] = "Password has been changed successfully.";

                user.Password = Encrypt(model.NewPassword);
                context.SaveChanges();

                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login", new { area = "" });
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