using System.Collections.Generic;
using BLL.Comments.Objects;

namespace BLL.Social.Journals.Objects
{
    public class JournalDisplayVm: JournalPreviewVm
    {

        public List<string> Tags { get; set; }

        public CommentsVm Comments { get; set; }
    }
}