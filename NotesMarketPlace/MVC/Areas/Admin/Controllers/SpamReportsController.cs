using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SpamReportsController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        public ActionResult SpamReports(string search, int? page, string sortBy)
        {
            var notes = context.SellerNotesReportedIssues.ToList().AsQueryable();

            ViewBag.SortByEditedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByReportedBy = sortBy == "ReportedBy desc" ? "ReportedBy" : "ReportedBy desc";
            ViewBag.SortByRemarks = sortBy == "Remarks desc" ? "Remarks" : "Remarks desc";

            bool isEmpty = !notes.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return View();
            }
            else if (notes.Where(x => x.NoteDetails.NoteTitle == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteDetails.NoteTitle == search || search == null);
            }
            else if (notes.Where(x => x.NoteDetails.NoteCategories.CategoryName == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteDetails.NoteCategories.CategoryName == search || search == null);
            }
            else if (notes.Where(x => x.Users.FirstName == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users.FirstName == search || search == null);
            }
            else if (notes.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                notes = notes.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (notes.Where(x => x.Remarks == search).ToList().Any())
            {
                notes = notes.Where(x => x.Remarks == search || search == null);
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
                    notes = notes.OrderByDescending(x => x.NoteDetails.NoteTitle);
                    break;

                case "NoteTitle":
                    notes = notes.OrderBy(x => x.NoteDetails.NoteTitle);
                    break;

                case "Category desc":
                    notes = notes.OrderByDescending(x => x.NoteDetails.NoteCategories.CategoryName);
                    break;

                case "Category":
                    notes = notes.OrderBy(x => x.NoteDetails.NoteCategories.CategoryName);
                    break;

                case "ReportedBy desc":
                    notes = notes.OrderByDescending(x => x.Users.FirstName);
                    break;

                case "ReportedBy":
                    notes = notes.OrderBy(x => x.Users.FirstName);
                    break;

                case "Remarks desc":
                    notes = notes.OrderByDescending(x => x.Remarks);
                    break;

                case "Remarks":
                    notes = notes.OrderBy(x => x.Remarks);
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.CreatedDate);
                    break;
            }
          
            return View(notes.ToPagedList(page ?? 1, 5));
        }

        public ActionResult DeleteSpamReport(int id)
        {
            SellerNotesReportedIssues note = context.SellerNotesReportedIssues.Where(x=>x.NoteID == id).FirstOrDefault();

            context.SellerNotesReportedIssues.Remove(note);
            context.SaveChanges();

            return RedirectToAction("SpamReports");
        }
    }
}