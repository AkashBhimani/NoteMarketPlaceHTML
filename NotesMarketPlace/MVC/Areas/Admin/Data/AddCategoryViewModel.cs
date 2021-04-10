using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class AddCategoryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"^[A-Za-z]*\S$", ErrorMessage = "Numeric and space entry should not be allowed.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "This is required field")]
        public string Description { get; set; }
    }
}