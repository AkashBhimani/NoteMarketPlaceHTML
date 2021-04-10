using NotesMarketPlace.Areas.Admin.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NotesController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        public IQueryable<NoteDetails> GetNotesUnderReview(string search, string sortBy)
        {
            IQueryable<NoteDetails> notes = context.NoteDetails.Where(x=>x.Status == "Submit for Review" || x.Status == "In Review").ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortBySeller = sortBy == "Seller desc" ? "Seller" : "Seller desc";
            ViewBag.SortByStatus = sortBy == "Status desc" ? "Status" : "Status desc";

            bool isEmpty = !notes.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return null;
            }
            else if (notes.Where(x => x.NoteTitle == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteTitle == search || search == null);
            }
            else if (notes.Where(x => x.NoteCategories.CategoryName == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteCategories.CategoryName == search || search == null);
            }
            else if (notes.Where(x => x.Users1.FirstName == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users1.FirstName == search || search == null);
            }
            else if (notes.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                notes = notes.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (notes.Where(x => x.Status == search).ToList().Any())
            {
                notes = notes.Where(x => x.Status == search || search == null);
            }
            else if(notes.Where(x=>x.UserID.ToString() == search).ToList().Any())
            {
                notes = notes.Where(x => x.UserID.ToString() == search || search == null);
            }
            else if (search != "" && search != null)
            {
                return null;
            }

            switch (sortBy)
            {
                case "Date":
                    notes = notes.OrderByDescending(x => x.CreatedDate);
                    break;

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

                case "Seller desc":
                    notes = notes.OrderByDescending(x => x.Users1.FirstName);
                    break;

                case "Seller":
                    notes = notes.OrderBy(x => x.Users1.FirstName);
                    break;

                case "Status desc":
                    notes = notes.OrderByDescending(x => x.Status);
                    break;

                case "Status":
                    notes = notes.OrderBy(x => x.Status);
                    break;


                default:
                    notes = notes.OrderBy(x => x.CreatedDate);
                    break;
            }
            return notes;
        }

        public ActionResult NotesUnderReview(string search, int? page, string sortBy)
        {
            var seller = context.NoteDetails.Select(x => x.Users1.FirstName).Distinct().ToList();
            var notes = GetNotesUnderReview(search, sortBy);
            NotesUnderReviewViewModel model;

            model = new NotesUnderReviewViewModel()
            {
                Notes = notes.ToPagedList(page ?? 1, 5),
                Seller = seller
            };

            return View(model);
        }

        public ActionResult NotesUnderReviewBySeller(string search, int? page, string sortBy)
        {
            var seller = context.NoteDetails.Select(x => x.Users1.FirstName).Distinct().ToList();
            var notes = GetNotesUnderReview(search, sortBy);            

            NotesUnderReviewViewModel model = new NotesUnderReviewViewModel()
            {
                Notes = notes.ToPagedList(page ?? 1, 5),
                Seller = seller
            };

            if (notes == null)
            {
                NotesUnderReview(search, page, sortBy);
            }

            return PartialView("_UnderReview", notes.ToPagedList(page ?? 1, 5));
        }

        public ActionResult ChangeStatusToInReview(int id)
        {
            var emailId = User.Identity.Name.ToString();
            var loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            NoteDetails note = context.NoteDetails.Where(x => x.ID == id).FirstOrDefault();

            if (note.Status == "Submit for Review")
            {
                note.Status = "In Review";
                note.ModifiedDate = DateTime.Now;
                note.ModifiedBy = loggedInUser.ID;
                context.SaveChanges();
            }

            return RedirectToAction("NotesUnderReview");
        }

        public ActionResult ChangeStatusToPublished(int id)
        {
            var emailId = User.Identity.Name.ToString();
            var loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            NoteDetails note = context.NoteDetails.Where(x => x.ID == id).FirstOrDefault();

            if (note.Status == "In Review" || note.Status == "Submit for Review")
            {
                note.Status = "Published";
                note.PublishedDate = DateTime.Now;
                note.ActionBy = loggedInUser.ID;
                note.ModifiedDate = DateTime.Now;
                note.ModifiedBy = loggedInUser.ID;
                context.SaveChanges();

                return RedirectToAction("NotesUnderReview");
            }
            else
            {
                note.Status = "Published";
                note.PublishedDate = DateTime.Now;
                note.ActionBy = loggedInUser.ID;
                note.ModifiedDate = DateTime.Now;
                note.ModifiedBy = loggedInUser.ID;
                context.SaveChanges();

                return RedirectToAction("RejectedNotes");
            }            
        }

        public ActionResult ChangeStatusToRejected(int id,string remarks)
        {
            var emailId = User.Identity.Name.ToString();
            var loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            NoteDetails note = context.NoteDetails.Where(x => x.ID == id).FirstOrDefault();

            if (note.Status == "In Review" || note.Status == "Submit for Review")
            {
                note.Status = "Rejected";
                note.ActionBy = loggedInUser.ID;
                note.Remarks = remarks;
                note.ModifiedDate = DateTime.Now;
                note.ModifiedBy = loggedInUser.ID;
                context.SaveChanges();
            }

            return RedirectToAction("NotesUnderReview");
        }

        public ActionResult NoteDetails(int id)
        {
            string emailId = User.Identity.Name;
            ViewBag.buyer = context.Users.Where(x => x.EmailID == emailId).Select(x => x.FirstName).FirstOrDefault();

            NoteDetails noteDetails = context.NoteDetails.Where(x => x.ID == id).FirstOrDefault();

            return View(noteDetails);
        }

        public bool DownloadNote(int id)
        {
            SellerNotesAttachments note = context.SellerNotesAttachments.Where(x => x.NoteID == id).FirstOrDefault();
            string _Path = note.FilePath;

            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + note.FileName);
            Response.TransmitFile(_Path);
            Response.End();

            return true;
        }

        public ActionResult RemoveReview(int id)
        {
            NoteReview review = context.NoteReview.Where(x => x.ID == id).FirstOrDefault();

            context.NoteReview.Remove(review);
            context.SaveChanges();

            return RedirectToAction("NoteDetails", new { id = review.NoteID });
        }


        public IQueryable<NoteDetails> GetPublishedNotes(string search, string sortBy)
        {
            IQueryable<NoteDetails> notes = context.NoteDetails.Where(x => x.Status == "Published").ToList().AsQueryable();

            ViewBag.SortByPublishedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByIsPaid = sortBy == "IsPaid desc" ? "IsPaid" : "IsPaid desc";
            ViewBag.SortBySellprice = sortBy == "SellPrice desc" ? "SellPrice" : "SellPrice desc";
            ViewBag.SortBySeller = sortBy == "Seller desc" ? "Seller" : "Seller desc";
            ViewBag.SortByApprovedBy = sortBy == "ApprovedBy desc" ? "ApprovedBy" : "ApprovedBy desc";
            ViewBag.SortByNoOfDownloads = sortBy == "NoOfDownloads desc" ? "NoOfDownloads" : "NoOfDownloads desc";

            bool isEmpty = !notes.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return null;
            }
            else if (notes.Where(x => x.NoteTitle == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteTitle == search || search == null);
            }
            else if (notes.Where(x => x.NoteCategories.CategoryName == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteCategories.CategoryName == search || search == null);
            }
            else if (search == "Paid" || search == "Free")
            {
                search = search == "Paid" ? "True" : "False";
                notes = notes.Where(x => x.IsPaid.ToString() == search || search == null);
            }
            else if (notes.Where(x => x.SellPrice.ToString() == search).ToList().Any())
            {
                notes = (notes.Where(x => x.SellPrice.ToString() == search || search == null));
            }
            else if (notes.Where(x => x.Users1.FirstName == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users1.FirstName == search || search == null);
            }
            else if (notes.Where(x => x.Users.FirstName == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users.FirstName == search || search == null);
            }
            else if (notes.Where(x => x.PublishedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                notes = notes.Where(x => x.PublishedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (notes.Where(x=>x.DownloadedNotes.Where(y=>y.NoteID == x.ID && y.IsAttachmentDownloaded == true).Count().ToString() == search).ToList().Any())
            {
                notes = notes.Where(x => x.DownloadedNotes.Where(y => y.NoteID == x.ID && y.IsAttachmentDownloaded == true).Count().ToString() == search || search == null);
            }
            else if (notes.Where(x => x.UserID.ToString() == search).ToList().Any())
            {
                notes = notes.Where(x => x.UserID.ToString() == search || search == null);
            }
            else if (search != "" && search != null)
            {
                return null;
            }

            switch (sortBy)
            {
                case "Date":
                    notes = notes.OrderBy(x => x.PublishedDate);
                    break;

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

                case "IsPaid desc":
                    notes = notes.OrderByDescending(x => x.IsPaid);
                    break;

                case "IsPaid":
                    notes = notes.OrderBy(x => x.IsPaid);
                    break;

                case "SellPrice desc":
                    notes = notes.OrderByDescending(x => x.SellPrice);
                    break;

                case "SellPrice":
                    notes = notes.OrderBy(x => x.SellPrice);
                    break;

                case "Seller desc":
                    notes = notes.OrderByDescending(x => x.Users1.FirstName);
                    break;

                case "Seller":
                    notes = notes.OrderBy(x => x.Users1.FirstName);
                    break;

                case "ApprovedBy desc":
                    notes = notes.OrderByDescending(x => x.Users.FirstName);
                    break;

                case "ApprovedBy":
                    notes = notes.OrderBy(x => x.Users.FirstName);
                    break;

                case "NoOfDownloads desc":
                    notes = notes.OrderByDescending(x => x.DownloadedNotes.Where(y => y.NoteID == x.ID && y.IsAttachmentDownloaded == true).Count());
                    break;

                case "NoOfDownloads":
                    notes = notes.OrderBy(x => x.DownloadedNotes.Where(y => y.NoteID == x.ID && y.IsAttachmentDownloaded == true).Count());
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.PublishedDate);
                    break;
            }
            return notes;
        }

        public ActionResult PublishedNotes(string search, int? page, string sortBy)
        {
            var seller = context.NoteDetails.Select(x => x.Users1.FirstName).Distinct().ToList();
            var notes = GetPublishedNotes(search, sortBy);
            PublishedNotesViewModel model;

            model = new PublishedNotesViewModel()
            {
                Notes = notes.ToPagedList(page ?? 1, 5),
                Seller = seller
            };

            return View(model);
        }

        public ActionResult GetPublishedNotesBySeller(string search, int? page, string sortBy)
        {
            var seller = context.NoteDetails.Select(x => x.Users1.FirstName).Distinct().ToList();
            var notes = GetPublishedNotes(search, sortBy);

            PublishedNotesViewModel model = new PublishedNotesViewModel()
            {
                Notes = notes.ToPagedList(page ?? 1, 5),
                Seller = seller
            };

            if (notes == null)
            {
                PublishedNotes(search, page, sortBy);
            }

            return PartialView("_Published", notes.ToPagedList(page ?? 1, 5));
        }

        public bool ChangeStatusToRemoved(int id, string remarks)
        {
            var emailId = User.Identity.Name.ToString();
            var loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            NoteDetails note = context.NoteDetails.Where(x => x.ID == id).FirstOrDefault();

            if (note.Status == "Published")
            {
                note.Status = "Removed";
                note.ActionBy = loggedInUser.ID;
                note.Remarks = remarks;
                note.ModifiedDate = DateTime.Now;
                note.ModifiedBy = loggedInUser.ID;
                context.SaveChanges();

                NotifytSellerToUnpublishNote(note.Users1.FirstName, note.Users1.EmailID, note.NoteTitle, remarks);
            }

            return true;
        }

        public void NotifytSellerToUnpublishNote(string seller,string emailID,string notetitle,string remark)
        {
            string supportEmail = context.ManageSystemConfiguration.Select(x => x.SupportEmail).FirstOrDefault();
            var fromEmail = new MailAddress(supportEmail);
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Akash@046"; // Replace with actual password
            string subject = "Sorry! We need to remove your notes from our portal. ";

            string body = "<br /> Hello " + seller + ", " +
                " <br /><br /> We want to inform you that, your note " + notetitle + " has been removed from the portal." +
                " <br /> Please find our remarks as below - <br />" + remark +
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

        public IQueryable<NoteDetails> GetRejectedNotes(string search, string sortBy)
        {
            IQueryable<NoteDetails> notes = context.NoteDetails.Where(x => x.Status == "Rejected").ToList().AsQueryable();

            ViewBag.SortByEditedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortBySeller = sortBy == "Seller desc" ? "Seller" : "Seller desc";
            ViewBag.SortByRejectedBy = sortBy == "RejectedBy desc" ? "RejectedBy" : "RejectedBy desc";
            ViewBag.SortByRemarks = sortBy == "Remarks desc" ? "Remarks" : "Remarks desc";

            bool isEmpty = !notes.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return null;
            }
            else if (notes.Where(x => x.NoteTitle == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteTitle == search || search == null);
            }
            else if (notes.Where(x => x.NoteCategories.CategoryName == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteCategories.CategoryName == search || search == null);
            }
            else if (notes.Where(x => x.Users1.FirstName == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users1.FirstName == search || search == null);
            }
            else if (notes.Where(x => x.Users.FirstName == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users.FirstName == search || search == null);
            }
            else if (notes.Where(x => x.ModifiedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                notes = notes.Where(x => x.ModifiedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (notes.Where(x => x.Remarks == search).ToList().Any())
            {
                notes = notes.Where(x => x.Remarks == search || search == null);
            }
            else if (search != "" && search != null)
            {
                return null;
            }

            switch (sortBy)
            {
                case "Date":
                    notes = notes.OrderBy(x => x.ModifiedDate);
                    break;

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

                case "Seller desc":
                    notes = notes.OrderByDescending(x => x.Users1.FirstName);
                    break;

                case "Seller":
                    notes = notes.OrderBy(x => x.Users1.FirstName);
                    break;

                case "RejectedBy desc":
                    notes = notes.OrderByDescending(x => x.Users.FirstName);
                    break;

                case "RejectedBy":
                    notes = notes.OrderBy(x => x.Users.FirstName);
                    break;

                case "Remarks desc":
                    notes = notes.OrderByDescending(x=>x.Remarks);
                    break;

                case "Remarks":
                    notes = notes.OrderBy(x => x.Remarks);
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.ModifiedDate);
                    break;
            }
            return notes;
        }

        public ActionResult RejectedNotes(string search, int? page, string sortBy)
        {
            var seller = context.NoteDetails.Select(x => x.Users1.FirstName).Distinct().ToList();
            var notes = GetRejectedNotes(search, sortBy);
            RejectedNotesViewModel model;

            model = new RejectedNotesViewModel()
            {
                Notes = notes.ToPagedList(page ?? 1, 5),
                Seller = seller
            };

            return View(model);
        }

        public ActionResult GetRejectedNotesBySeller(string search, int? page, string sortBy)
        {
            var seller = context.NoteDetails.Select(x => x.Users1.FirstName).Distinct().ToList();
            var notes = GetRejectedNotes(search, sortBy);

            RejectedNotesViewModel model = new RejectedNotesViewModel()
            {
                Notes = notes.ToPagedList(page ?? 1, 5),
                Seller = seller
            };

            if (notes == null)
            {
                RejectedNotes(search, page, sortBy);
            }

            return PartialView("_Published", notes.ToPagedList(page ?? 1, 5));
        }

        public IQueryable<DownloadedNotes> GetDownloadedNotes(string search, string sortBy)
        {
            IQueryable<DownloadedNotes> notes = context.DownloadedNotes.Where(x => x.IsAttachmentDownloaded == true).ToList().AsQueryable();

            ViewBag.SortByDownloadedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByBuyer = sortBy == "Buyer desc" ? "Buyer" : "Buyer desc";
            ViewBag.SortBySeller = sortBy == "Seller desc" ? "Seller" : "Seller desc";
            ViewBag.SortByIsPaid = sortBy == "IsPaid desc" ? "IsPaid" : "IsPaid desc";
            ViewBag.SortByPrice = sortBy == "Price desc" ? "Price" : "Price desc";

            bool isEmpty = !notes.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return null;
            }
            else if (notes.Where(x => x.NoteTitle == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteTitle == search || search == null);
            }
            else if (notes.Where(x => x.Category == search).ToList().Any())
            {
                notes = notes.Where(x => x.Category == search || search == null);
            }
            else if (notes.Where(x => x.Users.FirstName == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users.FirstName == search || search == null);
            }
            else if (notes.Where(x => x.Users1.FirstName == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users1.FirstName == search || search == null);
            }
            else if (search == "Paid" || search == "Free")
            {
                search = search == "Paid" ? "True" : "False";
                notes = notes.Where(x => x.IsPaid.ToString() == search || search == null);
            }
            else if (notes.Where(x => x.Price.ToString() == search).ToList().Any())
            {
                notes = (notes.Where(x => x.Price.ToString() == search || search == null));
            }
            else if (notes.Where(x => x.AttachmentDownloadedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                notes = notes.Where(x => x.AttachmentDownloadedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (notes.Where(x => x.Seller.ToString() == search).ToList().Any())
            {
                notes = notes.Where(x => x.Seller.ToString() == search || search == null);
            }
            else if (search != "" && search != null)
            {
                return null;
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

                case "Seller desc":
                    notes = notes.OrderByDescending(x => x.Users1.FirstName);
                    break;

                case "Seller":
                    notes = notes.OrderBy(x => x.Users1.FirstName);
                    break;

                case "Buyer desc":
                    notes = notes.OrderByDescending(x => x.Users.FirstName);
                    break;

                case "Buyer":
                    notes = notes.OrderBy(x => x.Users.FirstName);
                    break;

                case "IsPaid desc":
                    notes = notes.OrderByDescending(x => x.IsPaid);
                    break;

                case "IsPaid":
                    notes = notes.OrderBy(x => x.IsPaid);
                    break;

                case "Price desc":
                    notes = notes.OrderByDescending(x => x.Price);
                    break;

                case "Price":
                    notes = notes.OrderBy(x => x.Price);
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.AttachmentDownloadedDate);
                    break;
            }
            return notes;
        }

        public ActionResult DownloadedNotes(string search, int? page, string sortBy)
        {
            var seller = context.DownloadedNotes.Select(x => x.Users1.FirstName).Distinct().ToList();
            var buyer = context.DownloadedNotes.Select(x => x.Users.FirstName).Distinct().ToList();
            var note = context.DownloadedNotes.Where(x => x.IsAttachmentDownloaded == true).Select(x => x.NoteTitle).Distinct().ToList();

            var notes = GetDownloadedNotes(search, sortBy);
            DownloadedNotesViewModel model;

            model = new DownloadedNotesViewModel()
            {
                Notes = notes.ToPagedList(page ?? 1, 5),
                Seller = seller,
                Buyer = buyer,
                Note = note
            };

            return View(model);
        }

        public ActionResult GetDownloadedNotesByFilter(string search, int? page, string sortBy)
        {
            var seller = context.DownloadedNotes.Select(x => x.Users1.FirstName).Distinct().ToList();
            var buyer = context.DownloadedNotes.Select(x => x.Users.FirstName).Distinct().ToList();
            var note = context.DownloadedNotes.Where(x => x.IsAttachmentDownloaded == true).Select(x => x.NoteTitle).Distinct().ToList();

            var notes = GetDownloadedNotes(search, sortBy);

            DownloadedNotesViewModel model = new DownloadedNotesViewModel()
            {
                Notes = notes.ToPagedList(page ?? 1, 5),
                Seller = seller,
                Buyer = buyer,
                Note = note
            };

            if (notes == null)
            {
                DownloadedNotes(search, page, sortBy);
            }

            return PartialView("_Downloaded", notes.ToPagedList(page ?? 1, 5));
        }
    }
}