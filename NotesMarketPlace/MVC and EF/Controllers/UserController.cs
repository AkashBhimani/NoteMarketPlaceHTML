using System.Linq;
using System.Web.Mvc;
using System.IO;
using PagedList;
using NotesMarketPlace.Models;
using System.Data.Entity;
using System.Web;

namespace NotesMarketPlace.Controllers
{
    public class UserController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        // Add Notes By User
        [Authorize]
        [Route("AddNotes")]
        public ActionResult AddNotes(int? ID)
        {
            AddNoteViewModel DropDownListItems = new AddNoteViewModel();

            var countries = context.Countries.ToList();
            var categories = context.NoteCategories.ToList();
            var types = context.NoteTypes.ToList();

            if (ID != null)
            {
                DropDownListItems = context.NoteDetails.Where(x=>x.ID == ID).Select(x =>
                new AddNoteViewModel()
                {
                    NoteTitle = x.NoteTitle,
                    CategoryID = x.CategoryID,
                    TypeID = x.TypeID,
                    NumberOfPages = x.NumberOfPages,
                    Description = x.Description,
                    CountryId = x.CountryId,
                    InstitutionName = x.InstitutionName,
                    IsPaid = x.IsPaid,
                    Course = x.Course,
                    CourseCode = x.CourseCode,
                    Professor = x.Professor
                }
                ).FirstOrDefault();
            }

            DropDownListItems.Countries = countries;
            DropDownListItems.Categories = categories;
            DropDownListItems.Types = types;

            return View(DropDownListItems);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddNoteDetails(AddNoteViewModel note)
        {
            if(ModelState.IsValid)
            {
                if(note.IsPaid == true && note.NotePreview == null)
                {
                    TempData["NotePreviewIsRequired"] = "Note Preview field is required if Note is Paid.";

                    return RedirectToAction("AddNotes");
                }

                //Get Loggedin User Id
                var emailId = User.Identity.Name.ToString();
                var user = context.Users.Where(m => m.EmailID == emailId).FirstOrDefault();

                //For create Directory
                var _FilePath = Path.Combine(Server.MapPath("~/Members"), user.ID.ToString());

                //Check Wheter Directory is already exists or not
                if (!Directory.Exists(_FilePath))
                {
                    Directory.CreateDirectory(_FilePath);
                }

                //Insert data into database table "NoteDetails"
                NoteDetails dobj = new NoteDetails()
                {
                    ID=note.ID,
                    UserID = user.ID,
                    NoteTitle = note.NoteTitle,
                    CountryId = note.CategoryID,
                    Description = note.Description,
                    IsPaid = note.IsPaid,
                    TypeID = note.TypeID,
                    NumberOfPages = note.NumberOfPages,
                    InstitutionName = note.InstitutionName,
                    Course = note.Course,
                    CourseCode = note.CourseCode,
                    CategoryID = note.CategoryID,
                    Professor = note.Professor,
                    SellPrice = note.SellPrice,
                    Status = "Draft",
                    CreatedDate = System.DateTime.Now,
                    CreatedBy = user.ID,
                    IsActive = true,
                    UploadNote = "sadlj"
                };

                if(note.ID > 0)
                {
                    context.Entry(dobj).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.NoteDetails.Add(dobj);
                    context.SaveChanges();
                }

                //For Create subdirectory
                string finalpath = Path.Combine(Server.MapPath("~/Members/" + user.ID), dobj.ID.ToString());

                //Check Wheter subdirectory is already exists or not
                if(!Directory.Exists(finalpath))
                {
                    Directory.CreateDirectory(finalpath);
                }

                //For Display Picture
                if(note.DisplayPicture != null && note.DisplayPicture.ContentLength > 0)
                {
                    string _FileName = Path.GetFileNameWithoutExtension(note.DisplayPicture.FileName);
                    string extension = Path.GetFileName(note.DisplayPicture.FileName);

                    _FileName = "DP_" + System.DateTime.Now.ToString("dd-MM-yyyy") + extension;

                    _FilePath = Path.Combine(Server.MapPath("~/Members/" + user.ID + "/" + dobj.ID), _FileName);
                    note.DisplayPicture.SaveAs(_FilePath);                  
                }

                //For Note Preview
                if (note.DisplayPicture != null && note.NotePreview!= null && note.NotePreview.ContentLength > 0)
                {
                    string _FileName = Path.GetFileNameWithoutExtension(note.NotePreview.FileName);
                    string extension = Path.GetFileName(note.NotePreview.FileName);

                    _FileName = "Preview_" + System.DateTime.Now.ToString("dd-MM-yyyy") + extension;

                    _FilePath = Path.Combine(Server.MapPath("~/Members/" + user.ID + "/" + dobj.ID), _FileName);
                    note.DisplayPicture.SaveAs(_FilePath);
                }

                //For Upload Notes
                SellerNotesAttachments noteAttach = new SellerNotesAttachments()
                {
                    NoteID = dobj.ID,
                    IsActive = true,
                    CreatedBy = dobj.ID,
                    CreatedDate = System.DateTime.Now
                };

                string uploadPath = Path.Combine(finalpath, "Attachment");
                //Check Wheter subdirectory is already exists or not
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                int count = 1;
                var uploadfilepath = "";
                var uploadfilename = "";

                foreach (HttpPostedFileBase file in note.UploadNote)
                {
                    string _FileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);

                    _FileName = "Attachment" + count + "_" + System.DateTime.Now.ToString("dd-MM-yyyy") + extension;

                    string final = Path.Combine(uploadPath, _FileName);
                    file.SaveAs(final);

                    uploadfilename += _FileName + ";";
                    uploadfilepath += Path.Combine(("/Members/" + user.ID + "/" + dobj.ID + "/Attachment/"), _FileName);

                    count++;

                }

                noteAttach.FileName = uploadfilename;
                noteAttach.FilePath = uploadfilepath;
                context.SellerNotesAttachments.Add(noteAttach);
                context.SaveChanges();

            }

            return RedirectToAction("AddNotes");
        }

        public ActionResult DeleteRecord(int id)
        {
            SellerNotesAttachments sellernote = context.SellerNotesAttachments.Where(x => x.NoteID == id).FirstOrDefault();
            NoteDetails note = context.NoteDetails.Where(x => x.ID == id).FirstOrDefault();
            string folderpath = Server.MapPath("~/Members/" + note.UserID + "/" + id);
            Directory.Delete(folderpath, recursive: true);

            context.SellerNotesAttachments.Remove(sellernote);
            context.NoteDetails.Remove(note);
            context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        //Buyer Request
        [Route("BuyersRequest")]
        [HttpGet]
        public ActionResult BuyersRequest(string search, int? page, string sortBy)
        {
            var notes = context.NoteDetails.Include(x => x.NoteCategories).ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
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
            else if (notes.Where(x => x.NoteCategories.CategoryName == search).ToList().Any())
            {
                notes = notes.Where(x => x.NoteCategories.CategoryName == search || search == null);
            }
            else if (notes.Where(x => x.SellPrice.ToString() == search).ToList().Any())
            {
                notes = (notes.Where(x => x.SellPrice.ToString() == search || search == null));
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

                case "SellPrice":
                    notes = notes.OrderBy(x => x.SellPrice);
                    break;

                case "SellPrice desc":
                    notes = notes.OrderByDescending(x => x.SellPrice);
                    break;

                default:
                    notes = notes.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return View(notes.ToPagedList(page ?? 1, 2));
        }

        //Dashboard
        [Route("Dashboard")]
        public ActionResult Dashboard(string search, int? page, string sortBy)
        {
            var notes = context.NoteDetails.Include(x => x.NoteCategories).ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByStatus = sortBy == "Status desc" ? "Status" : "Status desc";

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
            else if (notes.Where(x => x.Status == search).ToList().Any())
            {
                notes = (notes.Where(x => x.Status == search || search == null));
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

                default:
                    notes = notes.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return View(notes.ToPagedList(page ?? 1,5));
        }
    }
}