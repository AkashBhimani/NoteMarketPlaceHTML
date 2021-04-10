using NotesMarketPlace.Areas.Admin.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MembersController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        // GET: Admin/Members
        [Authorize(Roles = "Admin")]
        public ActionResult Members(string search, int? page, string sortBy)
        {
            var members = context.Users.Where(x => x.IsActive == true && x.RoleID == 3).ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByFirstName = sortBy == "FirstName desc" ? "FirstName" : "FirstName desc";
            ViewBag.SortByLastName = sortBy == "LastName desc" ? "LastName" : "LastName desc";
            ViewBag.SortByEmail = sortBy == "Email desc" ? "Email" : "Email desc";
            ViewBag.SortByNoteUnderReview = sortBy == "NoteUnderReview desc" ? "NoteUnderReview" : "NoteUnderReview desc";
            ViewBag.SortByPublishedNote = sortBy == "PublishedNote desc" ? "PublishedNote" : "PublishedNote desc";
            ViewBag.SortByDownloadedNote = sortBy == "DownloadedNote desc" ? "DownloadedNote" : "DownloadedNote desc";
            ViewBag.SortByTotalExpense = sortBy == "TotalExpense desc" ? "TotalExpense" : "TotalExpense desc";
            ViewBag.SortByTotalEarning = sortBy == "TotalEarning desc" ? "TotalEarning" : "TotalEarning desc";

            bool isEmpty = !members.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return View();
            }
            else if (members.Where(x => x.FirstName == search).ToList().Any())
            {
                members = members.Where(x => x.FirstName == search || search == null);
            }
            else if (members.Where(x => x.LastName == search).ToList().Any())
            {
                members = members.Where(x => x.LastName == search || search == null);
            }
            else if (members.Where(x => x.EmailID == search).ToList().Any())
            {
                members = members.Where(x => x.EmailID == search || search == null);
            }
            else if (members.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                members = members.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (members.Where(x => x.NoteDetails1.Where(y=>y.UserID == x.ID).Sum(y=>y.SellPrice).ToString() == search).ToList().Any())
            {
                members = members.Where(x => x.NoteDetails1.Where(y => y.UserID == x.ID).Sum(y => y.SellPrice).ToString() == search || search == null);
            }
            else if (members.Where(x => x.DownloadedNotes1.Where(y=>y.Seller == x.ID && y.IsAttachmentDownloaded == true).Sum(y=>y.Price).ToString() == search).ToList().Any())
            {
                members = members.Where(x => x.DownloadedNotes1.Where(y => y.Seller == x.ID && y.IsAttachmentDownloaded == true).Sum(y => y.Price).ToString() == search || search == null);
            }
            else if (search != "" && search != null)
            {
                return View();
            }

            switch (sortBy)
            {
                case "Date":
                    members = members.OrderBy(x => x.CreatedDate);
                    break;

                case "FirstName desc":
                    members = members.OrderByDescending(x => x.FirstName);
                    break;

                case "FirstName":
                    members = members.OrderBy(x => x.FirstName);
                    break;

                case "LastName desc":
                    members = members.OrderByDescending(x => x.LastName);
                    break;

                case "LastName":
                    members = members.OrderBy(x => x.LastName);
                    break;

                case "Email desc":
                    members = members.OrderByDescending(x => x.EmailID);
                    break;

                case "Email":
                    members = members.OrderBy(x => x.EmailID);
                    break;

                case "NoteUnderReview desc":
                    members = members.OrderByDescending(x => x.NoteDetails1.Where(y=>y.UserID == x.ID && (y.Status == "In Review" || y.Status == "Submit for Review")).Count());
                    break;

                case "NoteUnderReview":
                    members = members.OrderBy(x => x.NoteDetails1.Where(y => y.UserID == x.ID && (y.Status == "In Review" || y.Status == "Submit for Review")).Count());
                    break;

                case "PublishedNote desc":
                    members = members.OrderByDescending(x => x.NoteDetails1.Where(y => y.UserID == x.ID && (y.Status == "Published")).Count());
                    break;

                case "PublishedNote":
                    members = members.OrderBy(x => x.NoteDetails1.Where(y => y.UserID == x.ID && (y.Status == "Published")).Count());
                    break;

                case "DownloadedNote desc":
                    members = members.OrderByDescending(x => x.DownloadedNotes1.Where(y => y.Seller == x.ID && y.IsAttachmentDownloaded == true).Count());
                    break;

                case "DownloadedNote":
                    members = members.OrderBy(x => x.DownloadedNotes1.Where(y => y.Seller == x.ID && y.IsAttachmentDownloaded == true).Count());
                    break;

                case "TotalExpense desc":
                    members = members.OrderByDescending(x => x.NoteDetails1.Where(y => y.UserID == x.ID).Sum(y => y.SellPrice));
                    break;

                case "TotalExpense":
                    members = members.OrderBy(x => x.NoteDetails1.Where(y => y.UserID == x.ID).Sum(y => y.SellPrice));
                    break;

                case "TotalEarning desc":
                    members = members.OrderByDescending(x => x.DownloadedNotes1.Where(y => y.Seller == x.ID && y.IsAttachmentDownloaded == true).Sum(y => y.Price));
                    break;

                case "TotalEarning":
                    members = members.OrderBy(x => x.DownloadedNotes1.Where(y => y.Seller == x.ID && y.IsAttachmentDownloaded == true).Sum(y => y.Price));
                    break;

                default:
                    members = members.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return View(members.ToPagedList(page ?? 1, 5));
        }

        public ActionResult Deactive(int id)
        {
            Users user = context.Users.Where(x => x.ID == id).FirstOrDefault();
            user.IsActive = false;
            context.SaveChanges();

            List<NoteDetails> notes = context.NoteDetails.Where(x => x.UserID == id).ToList();      
            foreach(var item in notes)
            {
                item.Status = "Removed";
            }
            context.SaveChanges();

            return RedirectToAction("Members");
        }

        public ActionResult MemberDetails(int id,string search, int? page, string sortBy)
        {
            UserProfile user = context.UserProfile.Where(x => x.UserID == id).FirstOrDefault();
            var notes = context.NoteDetails.Where(x=>x.UserID == id && x.Status != "Draft").ToList().AsQueryable();
            MemberDetailsViewModel member;

            ViewBag.SortByAddedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByStatus = sortBy == "Status desc" ? "Status" : "Status desc";
            ViewBag.SortByTotalEarning = sortBy == "TotalEarning desc" ? "TotalEarning" : "TotalEarning desc";
            ViewBag.SortByPublished = sortBy == "Published desc" ? "Published" : "Published desc";

            bool isEmpty = !notes.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                member = new MemberDetailsViewModel()
                {
                    ID = id,
                    Member = user
                };
                return View(member);
            }
            else if (notes.Where(x => x.NoteTitle == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteTitle == search || search == null);
            }
            else if (notes.Where(x => x.NoteCategories.CategoryName == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteCategories.CategoryName == search || search == null);
            }
            else if (notes.Where(x => x.Status == search).ToList().Any())
            {
                notes = (notes.Where(x => x.Status == search || search == null));
            }
            else if (notes.Where(x => x.DownloadedNotes.Where(y => y.IsAttachmentDownloaded == true).Count().ToString() == search).ToList().Any())
            {
                notes = (notes.Where(x => x.DownloadedNotes.Where(y => y.IsAttachmentDownloaded == true).Count().ToString() == search || search == null));
            }
            else if (notes.Where(x => x.DownloadedNotes.Where(y=>y.IsAttachmentDownloaded == true).Sum(y=>y.Price).ToString() == search).ToList().Any())
            {
                notes = (notes.Where(x => x.DownloadedNotes.Where(y => y.IsAttachmentDownloaded == true).Sum(y => y.Price).ToString() == search || search == null));
            }
            else if (notes.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                notes = (notes.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null));
            }
            else if (search != "" && search != null)
            {
                member = new MemberDetailsViewModel()
                {
                    ID = id,
                    Member = user
                };
                return View(member);
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
                    notes = notes.OrderByDescending(x => x.NoteCategories.CategoryName);
                    break;

                case "Category":
                    notes = notes.OrderBy(x => x.NoteCategories.CategoryName);
                    break;

                case "Status desc":
                    notes = notes.OrderByDescending(x => x.Status);
                    break;

                case "Status":
                    notes = notes.OrderBy(x => x.Status);
                    break;

                case "TotalEarning desc":
                    notes = notes.OrderByDescending(x => x.DownloadedNotes.Where(y => y.IsAttachmentDownloaded == true).Sum(y => y.Price));
                    break;

                case "TotalEarning":
                    notes = notes.OrderBy(x => x.DownloadedNotes.Where(y => y.IsAttachmentDownloaded == true).Sum(y => y.Price));
                    break;

                case "Published desc":
                    notes = notes.OrderByDescending(x => x.PublishedDate);
                    break;

                case "Published":
                    notes = notes.OrderBy(x => x.PublishedDate);
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            member = new MemberDetailsViewModel()
            {
                ID = id,
                Member = user,
                Notes = notes.ToPagedList(page ?? 1, 5)
            };

            return View(member);
        }
    }
}