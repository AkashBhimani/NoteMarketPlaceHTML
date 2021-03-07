using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}