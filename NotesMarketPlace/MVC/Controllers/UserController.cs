using System.Linq;
using System.Web.Mvc;
using System.IO;
using PagedList;
using NotesMarketPlace.Models;
using System.Data.Entity;
using System.Web;
using System.Net.Mail;
using System.Net;

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
                    string extension = Path.GetExtension(note.DisplayPicture.FileName);

                    _FileName = "DP_" + System.DateTime.Now.ToString("dd-MM-yyyy") + extension;

                    _FilePath = Path.Combine(Server.MapPath("~/Members/" + user.ID + "/" + dobj.ID + "/"), _FileName);
                    note.DisplayPicture.SaveAs(_FilePath);

                    dobj.DisplayPicture = _FilePath;
                    context.SaveChanges();
                }

                //For Note Preview
                if (note.NotePreview!= null && note.NotePreview.ContentLength > 0)
                {
                    string _FileName = Path.GetFileNameWithoutExtension(note.NotePreview.FileName);
                    string extension = Path.GetExtension(note.NotePreview.FileName);

                    _FileName = "Preview_" + System.DateTime.Now.ToString("dd-MM-yyyy") + extension;

                    _FilePath = Path.Combine(Server.MapPath("~/Members/" + user.ID + "/" + dobj.ID + "/"), _FileName);
                    note.NotePreview.SaveAs(_FilePath);

                    dobj.NotePreview = _FilePath;
                    context.SaveChanges();
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
            var notes = context.DownloadedNotes.Where(x=>x.IsSellerHasAllowedDownload == false).ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByNoteTitle = sortBy == "NoteTitle desc" ? "NoteTitle" : "NoteTitle desc";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByBuyer = sortBy == "Buyer desc" ? "Buyer" : "Buyer desc";
            ViewBag.SortByPhoneNo = sortBy == "PhoneNo desc" ? "PhoneNo" : "PhoneNo desc";
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
            else if (notes.Where(x => x.Users.UserProfile.Select(y => y.PhoneNumber).FirstOrDefault() == search).ToList().Any())
            {
                notes = notes.Where(x => x.Users.UserProfile.Select(y => y.PhoneNumber).FirstOrDefault() == search || search == null);
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

                case "PhoneNo desc":
                    notes = notes.OrderByDescending(x => x.Users.UserProfile.Select(y => y.PhoneNumber).FirstOrDefault());
                    break;

                case "PhoneNo":
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

        public ActionResult SellerAllowsDownload(int id)
        {
            DownloadedNotes note = context.DownloadedNotes.Where(x => x.ID == id).FirstOrDefault();
            note.IsSellerHasAllowedDownload = true;

            int noteId = note.NoteID;
            SellerNotesAttachments noteAttach = context.SellerNotesAttachments.Where(x => x.NoteID == noteId).FirstOrDefault();
            note.AttachmentPath = noteAttach.FilePath;

            context.SaveChanges();
            SellerAllowsDownloadNote(note.Users.EmailID,note.Users.FirstName,note.Users1.FirstName);

            return RedirectToAction("BuyersRequest");
        }

        public void SellerAllowsDownloadNote(string emailId,string buyer,string seller)
        {
            var fromEmail = new MailAddress("akash.bhimani.046@gmail.com", "Akash Bhimani");
            var toEmail = new MailAddress(emailId);
            var fromEmailPassword = "Akash@046";
            string subject = $"{seller} Allows you to download a note";

            string body = "<br /> Hello " + buyer + ",<br /><br />" +
                " We would like to inform you that, " + seller + " " + "Allows you to download a note." +
                " Please login and see My Download tabs to download particular note." +
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

        //Dashboard
        [Authorize]
        [Route("Dashboard")]
        public ActionResult Dashboard(string search, int? page, string sortBy)
        {
            string emailId = User.Identity.Name;
            Users loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            ViewBag.mySoldNotes = context.DownloadedNotes.Where(x => x.IsSellerHasAllowedDownload == true && x.Users1.EmailID == emailId).Count();
            ViewBag.myBuyerRequest = context.DownloadedNotes.Where(x => x.IsSellerHasAllowedDownload == false && x.Users1.EmailID == emailId).Count();
            ViewBag.muDownloads = context.DownloadedNotes.Where(x => x.IsSellerHasAllowedDownload == true && x.Users.ID == loggedInUser.ID).Count();
            ViewBag.totleEarning = context.DownloadedNotes.Where(x => x.IsSellerHasAllowedDownload == true && x.Users1.EmailID == emailId).Sum(x => x.Price);

            var notes = context.NoteDetails.Where(x=>x.Users.ID == loggedInUser.ID).ToList().AsQueryable();

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