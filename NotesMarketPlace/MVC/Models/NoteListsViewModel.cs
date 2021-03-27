using PagedList;
using System.Collections.Generic;

namespace NotesMarketPlace.Models
{
    public class NoteListsViewModel
    {
        public List<NoteCategories> Categories { get; set; }

        public List<NoteTypes> Types { get; set; }

        public List<string> University { get; set; }

        public List<string> Course { get; set; }

        public List<Countries> Countries { get; set; }

        public int Rating { get; set; }

        public IPagedList<NoteDetails> NoteDetails { get; set; }
    }
}