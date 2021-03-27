using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class AddNoteViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please select Note Category.")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please select Note Type.")]
        public int TypeID { get; set; }

        [Required(ErrorMessage = "Please select your Country.")]
        public int CountryId { get; set; }

        [Required(ErrorMessage ="This is required field")]
        public string NoteTitle { get; set; }

        public HttpPostedFileBase DisplayPicture { get; set; }

        [Required(ErrorMessage = "This is required field")]
        public HttpPostedFileBase[] UploadNote { get; set; }

        public Nullable<int> NumberOfPages { get; set; }

        [Required(ErrorMessage ="This is required field")]
        public string Description { get; set; }

        public string University { get; set; }

        public string InstitutionName { get; set; }

        public string Course { get; set; }

        public string CourseCode { get; set; }

        public string Professor { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        public decimal SellPrice { get; set; }

        public HttpPostedFileBase NotePreview { get; set; }

        public List<Countries> Countries { get; set; }

        public List<NoteCategories> Categories { get; set; }

        public List<NoteTypes> Types { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}