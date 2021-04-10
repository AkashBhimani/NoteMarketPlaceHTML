using NotesMarketPlace.Models;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace NotesMarketPlace.Controllers
{
    public class UserAccountController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        [Authorize]
        [Route("UserProfile")]
        public ActionResult UserProfile(int? id)
        {
            var emailId = User.Identity.Name.ToString();
            var countries = context.Countries.ToList();
            UserProfileViewModel loggedInUser = new UserProfileViewModel();

            loggedInUser = context.Users.Where(x => x.EmailID == emailId).Select(x =>
            new UserProfileViewModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailID = x.EmailID
            }).FirstOrDefault();

            //Check if Controller is comes for Update or not
            if (id != null)
            {
                loggedInUser = context.UserProfile.Where(x => x.ID == id).Select(x =>
                new UserProfileViewModel
                {
                    FirstName = loggedInUser.FirstName,
                    LastName = loggedInUser.LastName,
                    EmailID = loggedInUser.EmailID,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender,
                    PhoneNumber_CountryCode = x.PhoneNumber_CountryCode,
                    PhoneNumber = x.PhoneNumber,
                    AddressLine_1 = x.AddressLine_1,
                    AddressLine_2 = x.AddressLine_2,
                    City = x.City,
                    State = x.State,
                    ZipCode = x.ZipCode,
                    Country = x.Country,
                    University = x.University,
                    College = x.College
                }).FirstOrDefault();
            }

            loggedInUser.Countries = countries;

            return View(loggedInUser);
        }

        [HttpPost]
        public ActionResult AddUserProfileInformation(UserProfileViewModel model)
        {
            var emailId = User.Identity.Name.ToString();
            var user = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                //Profile Picture must be less than 10MB
                if (model.ProfilePicture != null && model.ProfilePicture.ContentLength > 10485760)
                {
                    TempData["ProfilePicValidation"] = "You can only upload picture up to 10MB";
                    return RedirectToAction("UserProfile");
                }

                //Store data into UserProfile table
                UserProfile obj = new UserProfile()
                {
                    ID=model.ID,
                    UserID = user.ID,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    PhoneNumber_CountryCode = model.PhoneNumber_CountryCode,
                    PhoneNumber = model.PhoneNumber,
                    AddressLine_1 = model.AddressLine_1,
                    AddressLine_2 = model.AddressLine_2,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    Country = model.Country,
                    University = model.University,
                    College = model.College,
                    SubmittedBy = user.ID,
                    SubmittedDate = DateTime.Now
                };

                //Store or update data into database
                if (model.ID > 0)
                {
                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.UserProfile.Add(obj);
                    context.SaveChanges();
                }

                //For create Directory
                string _FilePath = Path.Combine(Server.MapPath("~/Members"), user.ID.ToString());

                //Check Wheter subdirectory is already exists or not
                if (!Directory.Exists(_FilePath))
                {
                    Directory.CreateDirectory(_FilePath);
                }

                //For Profile Picture
                if (model.ProfilePicture != null && model.ProfilePicture.ContentLength > 0)
                {
                    string _FileName = Path.GetFileNameWithoutExtension(model.ProfilePicture.FileName);
                    string extension = Path.GetExtension(model.ProfilePicture.FileName);

                    _FileName = "DP_" + System.DateTime.Now.ToString("dd-MM-yyyy") + extension;

                    _FilePath = Path.Combine(Server.MapPath("~/Members/" + user.ID), _FileName);
                    model.ProfilePicture.SaveAs(_FilePath);

                    obj.ProfilePicture = _FilePath;
                    context.SaveChanges();
                }

            }

            return RedirectToAction("NoteList", "SearchNotes");
        }

        [Route("MyDownloads")]
        public ActionResult MyDownloads(string search, int? page, string sortBy)
        {
            string emailId = User.Identity.Name;
            var notes = context.DownloadedNotes.Where(x=>x.IsSellerHasAllowedDownload == true && x.Users.EmailID == emailId).ToList().AsQueryable();

            ViewBag.SortByDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByBuyer = sortBy == "Buyer desc" ? "Buyer" : "Buyer desc";
            ViewBag.SortByIsPaid = sortBy == "IsPaid desc" ? "IsPaid" : "IsPaid desc";
            ViewBag.SortBySellprice = sortBy == "SellPrice desc" ? "SellPrice" : "SellPrice desc";

            bool isEmpty = !notes.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return View();
            }
            else if (notes.Where(x => x.NoteTitle == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteTitle == search || search == null);
            }
            else if (notes.Where(x => x.Category == search).ToList().Any())
            {
                notes = notes.Where(x => x.Category == search || search == null);
            }
            else if (notes.Where(x => x.Users.EmailID == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users.EmailID == search || search == null);
            }
            else if (notes.Where(x => x.Price.ToString() == search).ToList().Any())
            {
                notes = (notes.Where(x => x.Price.ToString() == search || search == null));
            }
            else if (search != "" && search != null)
            {
                return View();
            }

            switch (sortBy)
            {
                case "Date":
                    notes = notes.OrderBy(x => x.AttachmentDownloadedDate);
                    break;

                case "NoteTitle desc":
                    notes = notes.OrderByDescending(x => x.NoteTitle);
                    break;

                case "NoteTitle":
                    notes = notes.OrderBy(x => x.NoteTitle);
                    break;

                case "Category desc":
                    notes = notes.OrderByDescending(x => x.Category);
                    break;

                case "Category":
                    notes = notes.OrderBy(x => x.Category);
                    break;

                case "Buyer desc":
                    notes = notes.OrderByDescending(x => x.Users.EmailID);
                    break;

                case "Buyer":
                    notes = notes.OrderBy(x => x.Users.EmailID);
                    break;

                case "IsPaid desc":
                    notes = notes.OrderByDescending(x => x.IsPaid);
                    break;

                case "IsPaid":
                    notes = notes.OrderBy(x => x.IsPaid);
                    break;

                case "SellPrice":
                    notes = notes.OrderBy(x => x.Price);
                    break;

                case "SellPrice desc":
                    notes = notes.OrderByDescending(x => x.Price);
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.AttachmentDownloadedDate);
                    break;
            }

            return View(notes.ToPagedList(page ?? 1, 10));
        }

        [Route("MyRejectedNotes")]
        public ActionResult MyRejectedNotes(string search, int? page, string sortBy)
        {
            string emailId = User.Identity.Name;
            var loggrdInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            var notes = context.NoteDetails.Where(x=>x.Status == "Rejected" && x.Users1.EmailID == emailId ).ToList().AsQueryable();

            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";

            bool isEmpty = !notes.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return View();
            }
            else if (notes.Where(x => x.NoteTitle == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteTitle == search || search == null);
            }
            else if (notes.Where(x => x.NoteCategories.CategoryName == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteCategories.CategoryName == search || search == null);
            }
            else if (search != "" && search != null)
            {
                return View();
            }

            switch (sortBy)
            {
                case "NoteTitle desc":
                    notes = notes.OrderByDescending(x => x.NoteTitle);
                    break;

                case "NoteTitle":
                    notes = notes.OrderBy(x => x.NoteTitle);
                    break;

                case "Category desc":
                    notes = notes.OrderByDescending(x => x.NoteCategories.CategoryName);
                    break;

                case "Category":
                    notes = notes.OrderBy(x => x.NoteCategories.CategoryName);
                    break;

                case "Buyer desc":
                    notes = notes.OrderByDescending(x => x.Users.EmailID);
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return View(notes.ToPagedList(page ?? 1, 10));
        }

        [Route("MySoldNotes")]
        public ActionResult MySoldNotes(string search, int? page, string sortBy)
        {
            string emailId = User.Identity.Name;
            var notes = context.DownloadedNotes.Where(x => x.IsAttachmentDownloaded == true && x.Users1.EmailID == emailId).ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByBuyer = sortBy == "Buyer desc" ? "Buyer" : "Buyer desc";
            ViewBag.SortByIsPaid = sortBy == "IsPaid desc" ? "IsPaid" : "IsPaid desc";
            ViewBag.SortBySellprice = sortBy == "SellPrice desc" ? "SellPrice" : "SellPrice desc";

            bool isEmpty = !notes.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return View();
            }
            else if (notes.Where(x => x.NoteTitle == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteTitle == search || search == null);
            }
            else if (notes.Where(x => x.Category == search).ToList().Any())
            {
                notes = notes.Where(x => x.Category == search || search == null);
            }
            else if (notes.Where(x => x.Users.EmailID == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users.EmailID == search || search == null);
            }
            else if (notes.Where(x => x.Price.ToString() == search).ToList().Any())
            {
                notes = (notes.Where(x => x.Price.ToString() == search || search == null));
            }
            else if (search != "" && search != null)
            {
                return View();
            }

            switch (sortBy)
            {
                case "Date":
                    notes = notes.OrderBy(x => x.CreatedDate);
                    break;

                case "NoteTitle desc":
                    notes = notes.OrderByDescending(x => x.NoteTitle);
                    break;

                case "NoteTitle":
                    notes = notes.OrderBy(x => x.NoteTitle);
                    break;

                case "Category desc":
                    notes = notes.OrderByDescending(x => x.Category);
                    break;

                case "Category":
                    notes = notes.OrderBy(x => x.Category);
                    break;

                case "Buyer desc":
                    notes = notes.OrderByDescending(x => x.Users.EmailID);
                    break;

                case "Buyer":
                    notes = notes.OrderBy(x => x.Users.EmailID);
                    break;

                case "IsPaid desc":
                    notes = notes.OrderByDescending(x => x.IsPaid);
                    break;

                case "IsPaid":
                    notes = notes.OrderBy(x => x.IsPaid);
                    break;

                case "SellPrice":
                    notes = notes.OrderBy(x => x.Price);
                    break;

                case "SellPrice desc":
                    notes = notes.OrderByDescending(x => x.Price);
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return View(notes.ToPagedList(page ?? 1, 10));
        }

        public ActionResult DownloadNote(int id)
        {
            DownloadedNotes note = context.DownloadedNotes.Where(x => x.NoteID == id).FirstOrDefault();

            if(note.IsPaid == true)
            {
                note.AttachmentDownloadedDate = DateTime.Now;
                note.IsSellerHasAllowedDownload = false;
                note.IsAttachmentDownloaded = true;

                context.SaveChanges();
            }            

            string _Path = note.AttachmentPath;

            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + note.NoteTitle + ".pdf");
            Response.TransmitFile(_Path);
            Response.End();

            return RedirectToAction("MyDownloads");
        }

        public ActionResult DownloadRejectedNote(int id,string notetitle)
        {
            SellerNotesAttachments note = context.SellerNotesAttachments.Where(x => x.NoteID == id).FirstOrDefault();
            string _Path = note.FilePath;

            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + notetitle + ".pdf");
            Response.TransmitFile(_Path);
            Response.End();

            return RedirectToAction("MyRejectedNotes");
        }

        public ActionResult NoteReview(int id,int rate,string comment)
        {
            var note = context.DownloadedNotes.Where(x => x.ID == id).FirstOrDefault();

            NoteReview obj = new NoteReview()
            {
                NoteID = note.NoteID,
                ReviewedByID = note.Users.ID,
                AgainstDownloadsID = id,
                Rate = rate,
                Comment = comment,
                CreatedDate = DateTime.Now,
                CreatedBy = note.Users.ID
            };

            context.NoteReview.Add(obj);
            context.SaveChanges();

            return RedirectToAction("MyDownloads");
        }

        public ActionResult MarksAsInAppropriate(int id,string remarks)
        {
            var note = context.DownloadedNotes.Where(x => x.ID == id).FirstOrDefault();

            SellerNotesReportedIssues obj = new SellerNotesReportedIssues()
            {
                NoteID = note.NoteID,
                ReportedByID = note.Users.ID,
                AgainstDownloadID = id,
                Remarks = remarks,
                CreatedDate = DateTime.Now,
                CreatedBy = note.Users.ID
            };

            context.SellerNotesReportedIssues.Add(obj);
            context.SaveChanges();

            NotifyAdminToSpamNote(note.Users.FirstName,note.Users1.FirstName,note.NoteTitle);

            return RedirectToAction("MyDownloads");
        }

        public void NotifyAdminToSpamNote(string member,string seller,string noteTitle)
        {
            string supportEmailID = context.ManageSystemConfiguration.Select(x => x.SupportEmail).FirstOrDefault();
            string adminEmailID = context.ManageSystemConfiguration.Select(x => x.EmailAddress).FirstOrDefault();
            var fromEmail = new MailAddress(supportEmailID);
            var toEmail = new MailAddress(adminEmailID);
            var fromEmailPassword = "******";
            string subject = $"{member} Reported an issue for {noteTitle}";

            string body = "<br /> Hello Admins, <br /><br />" +
                " We want to inform you that, " + member + " " + 
                " Reported an issue for"+ " " + seller +"’s Note with title " + noteTitle + " " +
                " Please look at the notes and take required actions. " +
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

        [Route("UserProfile/ChangePassword")]
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

                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

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