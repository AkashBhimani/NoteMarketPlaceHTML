using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class AddTypeViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This is required field")]
        public string Type { get; set; }

        [Required(ErrorMessage = "This is required field")]
        public string Description { get; set; }
    }
}