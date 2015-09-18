using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Social.Journals.Objects
{
    public class JournalVm
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public List<MediaVm> Media { get; set; }

        [Required]
        public List<string> Tags { get; set; }
    }
}