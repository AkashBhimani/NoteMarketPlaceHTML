using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using NotesMarketPlace.Areas.Admin.Data;
using PagedList;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    public class SettingController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        [Authorize(Roles = "Super Admin")]
        public ActionResult AddAdministrator(int? id)
        {
            AddAdministratorViewModel items = new AddAdministratorViewModel();
            var countries = context.Countries.Distinct().ToList();

            if (id != null)
            {
                items = context.Users.Where(x => x.ID == id).Select(x =>
                  new AddAdministratorViewModel
                  {
                      FirstName = x.FirstName,
                      LastName = x.LastName,
                      EmailID = x.EmailID,
                      PhoneNumber_CountryCode = x.PhoneNumber_CountryCode,
                      PhoneNumber = x.PhoneNumber
                  }).FirstOrDefault();
            }

            items.Countries = countries;

            return View(items);
        }

        [HttpPost]
        public ActionResult AddAdministrator(AddAdministratorViewModel model)
        {
            string emailId = User.Identity.Name;
            Users loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            Users obj = new Users();

            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    obj.RoleID = 2;
                    obj.FirstName = model.FirstName;
                    obj.LastName = model.LastName;
                    obj.Password = Encrypt("admin@123");
                    obj.EmailID = model.EmailID;
                    obj.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    obj.PhoneNumber = model.PhoneNumber;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = loggedInUser.ID;

                    context.Users.Add(obj);
                    context.SaveChanges();

                    SendVerificationLinkEmail(obj.EmailID);
                }
                else
                {
                    obj = context.Users.Where(x => x.ID == model.ID).FirstOrDefault();

                    obj.ID = model.ID;
                    obj.RoleID = 2;
                    obj.FirstName = model.FirstName;
                    obj.LastName = model.LastName;
                    obj.EmailID = model.EmailID;
                    obj.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    obj.PhoneNumber = model.PhoneNumber;
                    obj.IsActive = true;
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = loggedInUser.ID;

                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("ManageAdministrator");
                }

            }

            return RedirectToAction("AddAdministrator");
        }

        [Authorize(Roles = "Super Admin")]
        public ActionResult ManageAdministrator(string search, int? page, string sortBy)
        {
            var admins = context.Users.Where(x => x.RoleID == 2).ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByFirstName = sortBy == "FirstName desc" ? "FirstName" : "FirstName desc";
            ViewBag.SortByLastName = sortBy == "LastName desc" ? "LastName" : "LastName desc";
            ViewBag.SortByEmail = sortBy == "Email desc" ? "Email" : "Email desc";
            ViewBag.SortByPhoneNumber = sortBy == "PhoneNumber desc" ? "PhoneNumber" : "PhoneNumber desc";
            ViewBag.SortByActive = sortBy == "Active desc" ? "Active" : "Active desc";

            bool isEmpty = !admins.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return View();
            }
            else if (admins.Where(x => x.FirstName == search).ToList().Any())
            {
                admins = admins.Where(x => x.FirstName == search || search == null);
            }
            else if (admins.Where(x => x.LastName == search).ToList().Any())
            {
                admins = admins.Where(x => x.LastName == search || search == null);
            }
            else if (admins.Where(x => x.EmailID == search).ToList().Any())
            {
                admins = admins.Where(x => x.EmailID == search || search == null);
            }
            else if (admins.Where(x => x.PhoneNumber == search).ToList().Any())
            {
                admins = admins.Where(x => x.PhoneNumber == search || search == null);
            }
            else if (admins.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                admins = admins.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (search == "Yes" || search == "No")
            {
                search = search == "Yes" ? "True" : "False";
                admins = admins.Where(x => x.IsActive.ToString() == search || search == null);

                if(admins == null)
                {
                    return null;
                }
            }
            else if (search != "" && search != null)
            {
                return View();
            }

            switch (sortBy)
            {
                case "Date":
                    admins = admins.OrderBy(x => x.CreatedDate);
                    break;

                case "FirstName desc":
                    admins = admins.OrderByDescending(x => x.FirstName);
                    break;

                case "FirstName":
                    admins = admins.OrderBy(x => x.FirstName);
                    break;

                case "LastName desc":
                    admins = admins.OrderByDescending(x => x.LastName);
                    break;

                case "LastName":
                    admins = admins.OrderBy(x => x.LastName);
                    break;

                case "Email desc":
                    admins = admins.OrderByDescending(x => x.EmailID);
                    break;

                case "Email":
                    admins = admins.OrderBy(x => x.EmailID);
                    break;

                case "PhoneNumber desc":
                    admins = admins.OrderByDescending(x => x.PhoneNumber);
                    break;

                case "PhoneNumber":
                    admins = admins.OrderBy(x => x.PhoneNumber);
                    break;

                case "Active desc":
                    admins = admins.OrderByDescending(x => x.IsActive);
                    break;

                case "Active":
                    admins = admins.OrderBy(x => x.IsActive);
                    break;

                default:
                    admins = admins.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return View(admins.ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Super Admin")]
        public ActionResult DeleteAdminitrator(int id)
        {
            Users admin = context.Users.Where(x => x.ID == id).FirstOrDefault();

            admin.IsActive = false;
            context.SaveChanges();

            return RedirectToAction("ManageAdministrator");
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult AddCategory(int? id)
        {
            if (id != null)
            {
                AddCategoryViewModel category = context.NoteCategories.Where(x => x.ID == id).Select(x =>
                new AddCategoryViewModel
                {
                    Category = x.CategoryName,
                    Description = x.Description
                }).FirstOrDefault();

                return View(category);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(AddCategoryViewModel model)
        {
            string emailId = User.Identity.Name;
            Users loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            NoteCategories obj = new NoteCategories();

            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    obj.CategoryName = model.Category;
                    obj.Description = model.Description;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = loggedInUser.ID;
                    obj.IsActive = true;

                    context.NoteCategories.Add(obj);
                    context.SaveChanges();
                }
                else
                {
                    obj = context.NoteCategories.Where(x => x.ID == model.ID).FirstOrDefault();

                    obj.ID = model.ID;
                    obj.CategoryName = model.Category;
                    obj.Description = model.Description;
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = loggedInUser.ID;
                    obj.IsActive = true;

                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("ManageCategory");
                }

            }

            ModelState.Clear();
            return View();
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult ManageCategory(string search, int? page, string sortBy)
        {
            var categories = context.NoteCategories.ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByCategoryName = sortBy == "CategoryName desc" ? "CategoryName" : "CategoryName desc";
            ViewBag.SortByDescription = sortBy == "Description desc" ? "Description" : "Description desc";
            ViewBag.SortByAdder = sortBy == "Adder desc" ? "Adder" : "Adder desc";
            ViewBag.SortByActive = sortBy == "Active desc" ? "Active" : "Active desc";

            bool isEmpty = !categories.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return View();
            }
            else if (categories.Where(x => x.CategoryName == search).ToList().Any())
            {
                categories = categories.Where(x => x.CategoryName == search || search == null);
            }
            else if (categories.Where(x => x.Description == search).ToList().Any())
            {
                categories = categories.Where(x => x.Description == search || search == null);
            }
            else if (categories.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                categories = categories.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (categories.Where(x => x.Users.FirstName == search).ToList().Any())
            {
                categories = categories.Where(x => x.Users.FirstName == search || search == null);
            }
            else if (search == "Yes" || search == "No")
            {
                search = search == "Yes" ? "True" : "False";
                categories = categories.Where(x => x.IsActive.ToString() == search || search == null);
            }
            else if (search != "" && search != null)
            {
                return View();
            }

            switch (sortBy)
            {
                case "Date":
                    categories = categories.OrderBy(x => x.CreatedDate);
                    break;

                case "CategoryName desc":
                    categories = categories.OrderByDescending(x => x.CategoryName);
                    break;

                case "CategoryName":
                    categories = categories.OrderBy(x => x.Description);
                    break;

                case "Description desc":
                    categories = categories.OrderByDescending(x => x.Description);
                    break;

                case "Description":
                    categories = categories.OrderBy(x => x.Description);
                    break;

                case "Adder desc":
                    categories = categories.OrderByDescending(x => x.Users.FirstName);
                    break;

                case "Adder":
                    categories = categories.OrderBy(x => x.Users.FirstName);
                    break;

                case "Active desc":
                    categories = categories.OrderByDescending(x => x.IsActive);
                    break;

                case "Active":
                    categories = categories.OrderBy(x => x.IsActive);
                    break;

                default:
                    categories = categories.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return View(categories.ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult DeleteCategory(int id)
        {
            NoteCategories category = context.NoteCategories.Where(x => x.ID == id).FirstOrDefault();

            category.IsActive = false;
            context.SaveChanges();

            return RedirectToAction("ManageCategory");
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult AddType(int? id)
        {
            if (id != null)
            {
                AddTypeViewModel type = context.NoteTypes.Where(x => x.ID == id).Select(x =>
                new AddTypeViewModel
                {
                    Type = x.Type,
                    Description = x.Description
                }).FirstOrDefault();

                return View(type);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddType(AddTypeViewModel model)
        {
            string emailId = User.Identity.Name;
            Users loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            NoteTypes obj = new NoteTypes();

            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    obj.Type = model.Type;
                    obj.Description = model.Description;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = loggedInUser.ID;
                    obj.IsActive = true;

                    context.NoteTypes.Add(obj);
                    context.SaveChanges();
                }
                else
                {
                    obj = context.NoteTypes.Where(x => x.ID == model.ID).FirstOrDefault();

                    obj.ID = model.ID;
                    obj.Type = model.Type;
                    obj.Description = model.Description;
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = loggedInUser.ID;

                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("ManageType");
                }

            }

            ModelState.Clear();
            return View();
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult ManageType(string search, int? page, string sortBy)
        {
            var types = context.NoteTypes.ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByType = sortBy == "Type desc" ? "Type" : "Type desc";
            ViewBag.SortByDescription = sortBy == "Description desc" ? "Description" : "Description desc";
            ViewBag.SortByAdder = sortBy == "Adder desc" ? "Adder" : "Adder desc";
            ViewBag.SortByActive = sortBy == "Active desc" ? "Active" : "Active desc";

            bool isEmpty = !types.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return View();
            }
            else if (types.Where(x => x.Type == search).ToList().Any())
            {
                types = types.Where(x => x.Type == search || search == null);
            }
            else if (types.Where(x => x.Description == search).ToList().Any())
            {
                types = types.Where(x => x.Description == search || search == null);
            }
            else if (types.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                types = types.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (types.Where(x => x.Users.FirstName == search).ToList().Any())
            {
                types = types.Where(x => x.Users.FirstName == search || search == null);
            }
            else if (search == "Yes" || search == "No")
            {
                search = search == "Yes" ? "True" : "False";
                types = types.Where(x => x.IsActive.ToString() == search || search == null);
            }
            else if (search != "" && search != null)
            {
                return View();
            }

            switch (sortBy)
            {
                case "Date":
                    types = types.OrderBy(x => x.CreatedDate);
                    break;

                case "Type desc":
                    types = types.OrderByDescending(x => x.Type);
                    break;

                case "Type":
                    types = types.OrderBy(x => x.Description);
                    break;

                case "Description desc":
                    types = types.OrderByDescending(x => x.Description);
                    break;

                case "Description":
                    types = types.OrderBy(x => x.Description);
                    break;

                case "Adder desc":
                    types = types.OrderByDescending(x => x.Users.FirstName);
                    break;

                case "Adder":
                    types = types.OrderBy(x => x.Users.FirstName);
                    break;

                case "Active desc":
                    types = types.OrderByDescending(x => x.IsActive);
                    break;

                case "Active":
                    types = types.OrderBy(x => x.IsActive);
                    break;

                default:
                    types = types.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return View(types.ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult DeleteType(int id)
        {
            NoteTypes category = context.NoteTypes.Where(x => x.ID == id).FirstOrDefault();

            category.IsActive = false;
            context.SaveChanges();

            return RedirectToAction("ManageType");
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult AddCountry(int? id)
        {
            if (id != null)
            {
                AddCountryViewModel type = context.Countries.Where(x => x.ID == id).Select(x =>
                new AddCountryViewModel
                {
                    CountryName = x.CountryName,
                    CountryCode = x.CountryCode
                }).FirstOrDefault();

                return View(type);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddCountry(AddCountryViewModel model)
        {
            string emailId = User.Identity.Name;
            Users loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            Countries obj = new Countries();

            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    obj.CountryName = model.CountryName;
                    obj.CountryCode = model.CountryCode;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = loggedInUser.ID;

                    context.Countries.Add(obj);
                    context.SaveChanges();
                }
                else
                {
                    obj = context.Countries.Where(x => x.ID == model.ID).FirstOrDefault();

                    obj.CountryName = model.CountryName;
                    obj.CountryCode = model.CountryCode;
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = loggedInUser.ID;

                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("ManageCountry");
                }

            }

            ModelState.Clear();
            return View();
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult ManageCountry(string search, int? page, string sortBy)
        {
            var countries = context.Countries.ToList().AsQueryable();

            ViewBag.SortByCreatedDate = string.IsNullOrEmpty(sortBy) ? "Date" : "";
            ViewBag.SortByCategory = sortBy == "Category desc" ? "Category" : "Category desc";
            ViewBag.SortByCategoryCode = sortBy == "CategoryCode desc" ? "CategoryCode" : "CategoryCode desc";
            ViewBag.SortByAdder = sortBy == "Adder desc" ? "Adder" : "Adder desc";
            ViewBag.SortByActive = sortBy == "Active desc" ? "Active" : "Active desc";

            bool isEmpty = !countries.Any();
            if (isEmpty)
            {
                ViewBag.Message = "No records Found.";
                return View();
            }
            else if (countries.Where(x => x.CountryName == search).ToList().Any())
            {
                countries = countries.Where(x => x.CountryName == search || search == null);
            }
            else if (countries.Where(x => x.CountryCode == search).ToList().Any())
            {
                countries = countries.Where(x => x.CountryCode == search || search == null);
            }
            else if (countries.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search).ToList().Any())
            {
                countries = countries.Where(x => x.CreatedDate.Value.ToString("dd-MM-yyyy, hh:mm") == search || search == null);
            }
            else if (countries.Where(x => x.Users.FirstName == search).ToList().Any())
            {
                countries = countries.Where(x => x.Users.FirstName == search || search == null);
            }
            else if (search == "Yes" || search == "No")
            {
                search = search == "Yes" ? "True" : "False";
                countries = countries.Where(x => x.IsActive.ToString() == search || search == null);
            }
            else if (search != "" && search != null)
            {
                return View();
            }

            switch (sortBy)
            {
                case "Date":
                    countries = countries.OrderBy(x => x.CreatedDate);
                    break;

                case "Category desc":
                    countries = countries.OrderByDescending(x => x.CountryName);
                    break;

                case "Category":
                    countries = countries.OrderBy(x => x.CountryName);
                    break;

                case "CategoryCode desc":
                    countries = countries.OrderByDescending(x => x.CountryCode);
                    break;

                case "CategoryCode":
                    countries = countries.OrderBy(x => x.CountryCode);
                    break;

                case "Adder desc":
                    countries = countries.OrderByDescending(x => x.Users.FirstName);
                    break;

                case "Adder":
                    countries = countries.OrderBy(x => x.Users.FirstName);
                    break;

                case "Active desc":
                    countries = countries.OrderByDescending(x => x.IsActive);
                    break;

                case "Active":
                    countries = countries.OrderBy(x => x.IsActive);
                    break;

                default:
                    countries = countries.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return View(countries.ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult DeleteCountry(int id)
        {
            Countries category = context.Countries.Where(x => x.ID == id).FirstOrDefault();

            category.IsActive = false;
            context.SaveChanges();

            return RedirectToAction("ManageCountry");
        }

        [Authorize(Roles = "Super Admin")]
        public ActionResult ManageSystemConfiguration(int? id)
        {
            if (id != null)
            {
                ManageSystemConfiguration obj = context.ManageSystemConfiguration.Where(x => x.ID == id).FirstOrDefault();

                ManageSystemConfigurationViewModel model = new ManageSystemConfigurationViewModel()
                {
                    SupportEmailAddress = obj.SupportEmail,
                    SupportContactNumber = obj.SupportContactNumber,
                    EmailAddress = obj.EmailAddress,
                    TwitterURL = obj.TwitterURL,
                    FacebookURL = obj.FacebookURL,
                    LinkedInURL = obj.LinkedInURL,
                };

                return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult ManageSystemConfiguration(ManageSystemConfigurationViewModel model)
        {
            string emailId = User.Identity.Name;
            Users loggedInUser = context.Users.Where(x => x.EmailID == emailId).FirstOrDefault();

            ManageSystemConfiguration obj = new ManageSystemConfiguration();

            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    obj.SupportEmail = model.SupportEmailAddress;
                    obj.SupportContactNumber = model.SupportContactNumber;
                    obj.EmailAddress = model.EmailAddress;
                    obj.FacebookURL = model.FacebookURL;
                    obj.TwitterURL = model.TwitterURL;
                    obj.LinkedInURL = model.LinkedInURL;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = loggedInUser.ID;

                    if (model.DefaultNoteImage != null && model.DefaultNoteImage.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileNameWithoutExtension(model.DefaultNoteImage.FileName);
                        string extension = Path.GetExtension(model.DefaultNoteImage.FileName);

                        _FileName = "DefaultDisplayPicture" + extension;
                        string _FilePath = Path.Combine(Server.MapPath("/DefaultImages/" + "/"), _FileName);

                        if (System.IO.File.Exists(_FilePath))
                        {
                            System.IO.File.Delete(_FilePath);
                        }

                        model.DefaultNoteImage.SaveAs(_FilePath);
                        obj.DefaultNoteDisplayImage = Path.Combine(("/DefaultImages/" + "/"), _FileName);
                    }

                    if (model.DefaultProfileImage != null && model.DefaultProfileImage.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileNameWithoutExtension(model.DefaultProfileImage.FileName);
                        string extension = Path.GetExtension(model.DefaultProfileImage.FileName);

                        _FileName = "DefaultProfilePicture" + extension;
                        string _FilePath = Path.Combine(Server.MapPath("/DefaultImages/" + "/"), _FileName);

                        if (System.IO.File.Exists(_FilePath))
                        {
                            System.IO.File.Delete(_FilePath);
                        }

                        model.DefaultProfileImage.SaveAs(_FilePath);
                        obj.DefaultProfilePicture = Path.Combine(("/DefaultImages/" + "/"), _FileName);
                    }

                    ModelState.Clear();
                    context.ManageSystemConfiguration.Add(obj);
                    context.SaveChanges();
                }
                else
                {
                    obj = context.ManageSystemConfiguration.Where(x => x.ID == model.ID).FirstOrDefault();

                    obj.ID = model.ID;
                    obj.SupportEmail = model.SupportEmailAddress;
                    obj.SupportContactNumber = model.SupportContactNumber;
                    obj.EmailAddress = model.EmailAddress;
                    obj.FacebookURL = model.FacebookURL;
                    obj.TwitterURL = model.TwitterURL;
                    obj.LinkedInURL = model.LinkedInURL;
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = loggedInUser.ID;

                    if (model.DefaultNoteImage != null && model.DefaultNoteImage.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileNameWithoutExtension(model.DefaultNoteImage.FileName);
                        string extension = Path.GetExtension(model.DefaultNoteImage.FileName);

                        _FileName = "DefaultDisplayPicture" + extension;
                        string _FilePath = Path.Combine(Server.MapPath("/DefaultImages/" + "/"), _FileName);

                        if (System.IO.File.Exists(_FilePath))
                        {
                            System.IO.File.Delete(_FilePath);
                        }

                        model.DefaultNoteImage.SaveAs(_FilePath);
                        obj.DefaultNoteDisplayImage = Path.Combine(("/DefaultImages/" + "/"), _FileName);
                    }

                    if (model.DefaultProfileImage != null && model.DefaultProfileImage.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileNameWithoutExtension(model.DefaultProfileImage.FileName);
                        string extension = Path.GetExtension(model.DefaultProfileImage.FileName);

                        _FileName = "DefaultProfilePicture" + extension;
                        string _FilePath = Path.Combine(Server.MapPath("/DefaultImages/" + "/"), _FileName);

                        if (System.IO.File.Exists(_FilePath))
                        {
                            System.IO.File.Delete(_FilePath);
                        }

                        model.DefaultProfileImage.SaveAs(_FilePath);
                        obj.DefaultProfilePicture = Path.Combine(("/DefaultImages/" + "/"), _FileName);
                    }

                    ModelState.Clear();
                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();
                }

            }

            return View();
        }

        public ActionResult EmailVerificationModal(string emailID)
        {
            var user = context.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
            TempData["UserName"] = user.FirstName;
            return View(user);
        }

        public void SendVerificationLinkEmail(string emailID)
        {
            var user = context.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
            var verifyUrl = "/Admin/Setting/EmailVerificationModal?emailID=" + user.EmailID;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("akash.bhimani.046@gmail.com", "Akash Bhimani");
            var toEmail = new MailAddress(user.EmailID);
            var fromEmailPassword = "Akash@046"; // Replace with actual password
            string subject = "Note Marketplace - Email Verification";

            string body = "<br /> Hello " + user.FirstName + ", <br /> Thank you for joining with NotesMarketPlace." +
                " <br /><br /> Your Password: admin@123 <br />" +
                " Please click on below link to verify your email address and to do login" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> " +
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

        public ActionResult VerifyAccount(string emailid)
        {
            var user = context.Users.Where(a => a.EmailID == emailid).FirstOrDefault();
            if (user != null)
            {
                user.IsEmailVerified = true;
                user.IsActive = true;
                context.SaveChanges();
            }
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