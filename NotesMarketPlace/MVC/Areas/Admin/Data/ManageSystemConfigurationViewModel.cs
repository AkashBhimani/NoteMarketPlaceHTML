using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class ManageSystemConfigurationViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string SupportEmailAddress { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}", ErrorMessage = "Please enter valid phone number.")]
        public string SupportContactNumber { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string EmailAddress { get; set; }

        public string FacebookURL { get; set; }

        public string TwitterURL { get; set; }

        public string LinkedInURL { get; set; }

        [Required(ErrorMessage = "This is required field")]
        public HttpPostedFileBase DefaultNoteImage { get; set; }

        [Required(ErrorMessage = "This is required field")]
        public HttpPostedFileBase DefaultProfileImage { get; set; }
    }
}