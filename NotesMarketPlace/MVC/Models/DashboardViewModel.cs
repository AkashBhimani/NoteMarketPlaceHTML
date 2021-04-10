using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class DashboardViewModel
    {
        public NoteDetails NoteDetails { get; set; }

        public NoteCategories NoteCategories { get; set; }
    }
}