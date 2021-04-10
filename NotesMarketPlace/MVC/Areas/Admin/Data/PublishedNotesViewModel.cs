using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class PublishedNotesViewModel
    {
        public List<string> Seller { get; set; }

        public IPagedList<NoteDetails> Notes { get; set; }
    }
}