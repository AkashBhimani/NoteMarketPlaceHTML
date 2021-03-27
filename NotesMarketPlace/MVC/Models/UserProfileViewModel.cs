using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class UserProfileViewModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailID { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateOfBirth { get; set; }

        public string Gender { get; set; }

        [Required]
        public string PhoneNumber_CountryCode { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}")]
        public string PhoneNumber { get; set; }

        public HttpPostedFileBase ProfilePicture { get; set; }

        [Required]
        public string AddressLine_1 { get; set; }

        [Required]
        public string AddressLine_2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Country { get; set; }

        public string University { get; set; }

        public string College { get; set; }

        public List<Countries> Countries { get; set; }

        public Nullable<System.DateTime> SubmittedDate { get; set; }

        public Nullable<int> SubmittedBy { get; set; }

    }
}