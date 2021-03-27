using System.ComponentModel.DataAnnotations;

namespace NotesMarketPlace.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"^[A-Za-z]*\S$", ErrorMessage = "Please enter valid First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"^[A-Za-z]*\S$", ErrorMessage = "Please enter valid Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!% *#?&])[A-Za-z\d@$!%*#?&]{6,24}$", ErrorMessage = "Please enter valid Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [Compare("Password", ErrorMessage = "Passwords do not matched.")]
        public string ConfirmPassword { get; set; }
    }
}