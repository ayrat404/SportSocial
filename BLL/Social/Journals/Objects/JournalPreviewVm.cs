using System;
using BLL.Blog.ViewModels;

namespace BLL.Social.Journals.Objects
{
    public class JournalPreviewVm
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public RatingInfo Rating { get; set; }
        
        public DateTime Created { get; set; }
    }
}