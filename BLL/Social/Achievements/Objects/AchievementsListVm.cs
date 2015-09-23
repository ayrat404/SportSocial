using System.Collections.Generic;

namespace BLL.Social.Achievements.Objects
{
    public class PagedListVm<T>
    {
        public IEnumerable<T> List { get; set; }

        public bool IsMore { get; set; }
    }
}