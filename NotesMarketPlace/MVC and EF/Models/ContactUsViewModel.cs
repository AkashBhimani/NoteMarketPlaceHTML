using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage ="This is required field")]
        [RegularExpression(@"^[A-za-z'\s]*$", ErrorMessage ="Please enter valid Full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [EmailAddress(ErrorMessage ="Please enter valid Email address")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "This is required field")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "This is required field")]
        public string Comments_Questions { get; set; }

    }
}