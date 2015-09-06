using System.Collections.Generic;
using BLL.Comments.Objects;

namespace BLL.Social.Journals.Objects
{
    public class JournalDisplayVm: JournalPreviewVm
    {
        public List<MediaVm> Media { get; set; }

        public List<string> Themes { get; set; }

        public List<Comment> Comments { get; set; }
    }
}