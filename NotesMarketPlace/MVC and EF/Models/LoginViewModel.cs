using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class LoginViewModel
    {
        public string EmailId { get; set; }

        public string Password { get; set; }

        public string RememberMe { get; set; }
    }
}