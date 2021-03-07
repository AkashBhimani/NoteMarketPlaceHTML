using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    public class ContactUsController : Controller
    {
        NotesMarketPlaceEntities context = new NotesMarketPlaceEntities();

        // GET: ContactUs
        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestions(ContactUsViewModel model)
        {
            if(ModelState.IsValid)
            {
                ContactUs obj = new ContactUs()
                {
                    FullName = model.FullName,
                    EmailID = model.EmailID,
                    Subject = model.Subject,
                    Comments_Questions = model.Comments_Questions
                };

                ModelState.Clear();
                context.ContactUs.Add(obj);
                context.SaveChanges();
            }
            return View("ContactUs");
        }
    }
}