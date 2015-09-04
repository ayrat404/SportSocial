using System.Collections.Generic;

namespace BLL.Social.Journals.Objects
{
    public class JournalVm
    {
        public string Text { get; set; }

        public MediaVm Media { get; set; }

        public List<string> Themes { get; set; }
    }
}