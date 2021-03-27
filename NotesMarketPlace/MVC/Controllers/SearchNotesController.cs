using NotesMarketPlace.Models;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    public class SearchNotesController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        //Display All Notes
        [AllowAnonymous]
        [Route("SearchNotes")]
        public ActionResult NoteList(string searchBy, int? page)
        {
            var countries = context.Countries.ToList();
            var categories = context.NoteCategories.ToList();
            var types = context.NoteTypes.ToList();
            var university = context.NoteDetails.Where(x => x.InstitutionName != null).Select(x => x.InstitutionName).ToList();
            var course = context.NoteDetails.Where(x => x.Course != null).Select(x => x.Course).Distinct().ToList();
            var notesDetails = context.NoteDetails.ToList().AsQueryable();
            ViewBag.count = context.NoteDetails.ToList().Count();

            NoteListsViewModel obj = new NoteListsViewModel()
            {
                Categories = categories,
                Countries = countries,
                Types = types,
                University = university,
                Course = course,
                NoteDetails = notesDetails.ToPagedList(page ?? 1, 9)
            };

            return View(obj);
        }

        //Filter Notes
        public ActionResult SearchNotes(string value, int? page)
        {
            var notesDetails = context.NoteDetails.ToList().AsQueryable();

            if (notesDetails.Where(x => x.NoteTypes.Type == value).ToList().Any())
            {
                notesDetails = notesDetails.Where(x => x.NoteTypes.Type == value);
                ViewBag.count = notesDetails.Count();
            }
            else if (notesDetails.Where(x => x.NoteCategories.CategoryName == value).ToList().Any())
            {
                notesDetails = notesDetails.Where(x => x.NoteCategories.CategoryName == value);
                ViewBag.count = notesDetails.Count();
            }
            else if (notesDetails.Where(x => x.InstitutionName == value).ToList().Any())
            {
                notesDetails = notesDetails.Where(x => x.InstitutionName == value);
                ViewBag.count = notesDetails.Count();
            }
            else if (notesDetails.Where(x => x.Course == value).ToList().Any())
            {
                notesDetails = notesDetails.Where(x => x.Course == value);
                ViewBag.count = notesDetails.Count();
            }
            else if (notesDetails.Where(x => x.Countries.CountryName == value).ToList().Any())
            {
                notesDetails = notesDetails.Where(x => x.Countries.CountryName == value);
                ViewBag.count = notesDetails.Count();
            }
            else if (notesDetails.Where(x => x.NoteTitle == value).ToList().Any())
            {
                notesDetails = notesDetails.Where(x => x.NoteTitle == value);
                ViewBag.count = notesDetails.Count();
            }            
            else if (value == "" || value == null)
            {
                return PartialView("_Notes", notesDetails.ToPagedList(page ?? 1, 9));
            }
            else
            {
                ViewBag.count = "No record found.";
                return PartialView("_Notes", null);
            }

            return PartialView("_Notes", notesDetails.ToPagedList(page ?? 1, 9));
        }

        [Route("searchnotes/NoteDetails")]
        public ActionResult NoteDetails(int id)
        {
            string emailId = User.Identity.Name;
            ViewBag.buyer = context.Users.Where(x => x.EmailID == emailId).Select(x=>x.FirstName).FirstOrDefault();

            NoteDetails noteDetails = context.NoteDetails.Where(x => x.ID == id).FirstOrDefault();

            return View(noteDetails);
        }

        public ActionResult MyDownloads(int id)
        {
            NoteDetails note = context.NoteDetails.Where(x => x.ID == id).FirstOrDefault();

            //check whether user is login or not
            if (User.Identity.IsAuthenticated)
            {
                string emailId = User.Identity.Name;
                var loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

                string _Path = @"~/Members/" + note.UserID + "/" + note.ID + "/Attachment/Attachment1_" + note.CreatedDate.Value.ToString("dd-MM-yyyy") + ".pdf";
                string title = note.NoteTitle;

                if (!note.IsPaid)
                {
                    //Download the note over browser
                    Response.Clear();
                    Response.ContentType = "Application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + note.NoteTitle + ".pdf");
                    Response.TransmitFile(_Path);
                    Response.End();

                    //Insert data in download table
                    DownloadedNotes obj = new DownloadedNotes()
                    {
                        NoteID = note.ID,
                        Seller = note.UserID,
                        Buyer = loggedInUser.ID,
                        IsSellerHasAllowedDownload = true,
                        AttachmentPath = _Path,
                        IsAttachmentDownloaded = true,
                        AttachmentDownloadedDate = System.DateTime.Now,
                        IsPaid = false,
                        Price = 0,
                        NoteTitle = note.NoteTitle,
                        Category = note.NoteCategories.CategoryName,
                        CreatedDate = System.DateTime.Now,
                        CreatedBy = loggedInUser.ID
                    };

                    context.DownloadedNotes.Add(obj);
                    context.SaveChanges();

                    return RedirectToAction("NoteDetails", new { id });

                }
                else
                {
                    DownloadedNotes obj = new DownloadedNotes()
                    {
                        NoteID = note.ID,
                        Seller = note.UserID,
                        Buyer = loggedInUser.ID,
                        IsSellerHasAllowedDownload = false,
                        IsAttachmentDownloaded = false,
                        AttachmentDownloadedDate = System.DateTime.Now,
                        IsPaid = true,
                        Price = note.SellPrice,
                        NoteTitle = note.NoteTitle,
                        Category = note.NoteCategories.CategoryName,
                        CreatedDate = System.DateTime.Now,
                        CreatedBy = note.UserID
                    };

                    context.DownloadedNotes.Add(obj);
                    context.SaveChanges();

                    notifySellerToAllowDownloadNote(note.Users.EmailID, loggedInUser.FirstName, note.Users.FirstName);

                    return RedirectToAction("NoteDetails", new { id });
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public void notifySellerToAllowDownloadNote(string emailID, string buyer, string seller)
        {
            var fromEmail = new MailAddress("akash.bhimani.046@gmail.com", "Akash Bhimani");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Akash@046";
            string subject = $"{buyer} wants to purchase your notes";

            string body = "<br /> Hello " + seller + ",<br /><br />" +
                " We would like to inform you that, " + buyer + " " + "wants to purchase your notes." +
                " Please see Buyer Requests tab and allow download access to Buyer if you have received the payment from him." +
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

    }
}

