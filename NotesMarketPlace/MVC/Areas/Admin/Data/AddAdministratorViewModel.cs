using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class AddAdministratorViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"^[A-Za-z]*\S$", ErrorMessage = "Please enter valid First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"^[A-Za-z]*\S$", ErrorMessage = "Please enter valid Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string EmailID { get; set; }

        [Required]
        public string PhoneNumber_CountryCode { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}",ErrorMessage = "Please enter valid phone number.")]
        public string PhoneNumber { get; set; }

        public List<Countries> Countries { get; set; }
    }
}