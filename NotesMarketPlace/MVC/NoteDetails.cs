//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotesMarketPlace
{
    using System;
    using System.Collections.Generic;
    
    public partial class NoteDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoteDetails()
        {
            this.DownloadedNotes = new HashSet<DownloadedNotes>();
            this.NoteReview = new HashSet<NoteReview>();
            this.SellerNotesReportedIssues = new HashSet<SellerNotesReportedIssues>();
            this.SpamReports = new HashSet<SpamReports>();
        }
    
        public int ID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public int TypeID { get; set; }
        public int CountryId { get; set; }
        public string NoteTitle { get; set; }
        public string DisplayPicture { get; set; }
        public string UploadNote { get; set; }
        public Nullable<int> NumberOfPages { get; set; }
        public string Description { get; set; }
        public string InstitutionName { get; set; }
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public string Professor { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public string NotePreview { get; set; }
        public string Status { get; set; }
        public Nullable<int> ActionBy { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> PublishedDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Countries Countries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DownloadedNotes> DownloadedNotes { get; set; }
        public virtual NoteCategories NoteCategories { get; set; }
        public virtual NoteTypes NoteTypes { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoteReview> NoteReview { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SellerNotesReportedIssues> SellerNotesReportedIssues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpamReports> SpamReports { get; set; }
    }
}
