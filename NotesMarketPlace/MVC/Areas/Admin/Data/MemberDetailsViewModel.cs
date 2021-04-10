using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class MemberDetailsViewModel
    {
        public int ID { get; set; }

        public UserProfile Member { get; set; }

        public IPagedList<NoteDetails> Notes { get; set; }
    }
}