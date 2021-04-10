using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class AddCountryViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"^[A-Za-z]*\S$", ErrorMessage = "Please enter valid countryname")]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "This is required field")]
        public string CountryCode { get; set; }
    }
}