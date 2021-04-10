using DocumentFormat.OpenXml.Bibliography;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();
       
        public IQueryable<NoteDetails> GetDashboardContent(string search, string sortBy)
        {
            var notes = context.NoteDetails.Where(x => x.Status == "Published").ToList().AsQueryable();

            ViewBag.SortByPublishedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByIsPaid = sortBy == "IsPaid desc" ? "IsPaid" : "IsPaid desc";
            ViewBag.SortByPrice = sortBy == "Price desc" ? "Price" : "Price desc";
            ViewBag.SortByPublisher = sortBy == "Publisher desc" ? "Publisher" : "Publisher desc";
            ViewBag.SortByNoOfDownload = sortBy == "NoOfDownload desc" ? "NoOfDownload" : "NoOfDownload desc";

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
            else if (notes.Where(x => x.Users.FirstName == search).ToList().Any())
            {
                notes = (notes.Where(x => x.Users.FirstName == search || search == null));
            }
            else if (notes.Where(x => x.PublishedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                notes = notes.Where(x => x.PublishedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (notes.Where(x => x.PublishedDate.Value.ToString("MMMM") == search).ToList().Any())
            {
                notes = notes.Where(x => x.PublishedDate.Value.ToString("MMMM") == search || search == null);
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

                case "Price desc":
                    notes = notes.OrderByDescending(x => x.SellPrice);
                    break;

                case "Price":
                    notes = notes.OrderBy(x => x.SellPrice);
                    break;

                case "Publisher desc":
                    notes = notes.OrderByDescending(x => x.Users.FirstName);
                    break;

                case "Publisher":
                    notes = notes.OrderBy(x => x.Users.FirstName);
                    break;

                case "NoOfDownload desc":
                    notes = notes.OrderByDescending(x => x.Users.FirstName);
                    break;

                case "NoOfDownload":
                    notes = notes.OrderBy(x => x.DownloadedNotes.Where(y=>y.IsAttachmentDownloaded == true).Count());
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.DownloadedNotes.Where(y => y.IsAttachmentDownloaded == true).Count());
                    break;
            }

            return notes;
        }

        public ActionResult Dashboard(string search, int? page, string sortBy)
        {
            ViewBag.NoOfNotesInReview = context.NoteDetails.Where(x => x.Status == "In Review" || x.Status == "Submit for Review").Count();
            var lastWeek = System.DateTime.Now.AddDays(-7);
            ViewBag.NoOfDownloadedNotes = context.DownloadedNotes.Where(x => x.IsAttachmentDownloaded == true && x.AttachmentDownloadedDate >= lastWeek).Count();
            ViewBag.NoOfRegistration = context.Users.Where(x =>x.RoleID == 3 && x.IsActive == true && x.CreatedDate >= lastWeek).Count();            

            var notes = GetDashboardContent(search, sortBy);
            return View(notes.ToPagedList(page ?? 1, 5));
        }

        public ActionResult GetPublishedNotesByFilter(string search, int? page, string sortBy)
        {
            var notes = GetDashboardContent(search, sortBy);

            if (notes == null)
            {
                Dashboard(search, page, sortBy);
            }

            return PartialView("_PublishedNotes", notes.ToPagedList(page ?? 1, 5));
        }
    }
}