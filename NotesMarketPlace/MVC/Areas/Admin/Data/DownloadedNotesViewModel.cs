using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class DownloadedNotesViewModel
    {
        public List<string> Seller { get; set; }

        public List<string> Buyer { get; set; }

        public List<string> Note { get; set; }

        public IPagedList<DownloadedNotes> Notes { get; set; }

    }
}