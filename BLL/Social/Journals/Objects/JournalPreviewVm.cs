using System;
using BLL.Blog.ViewModels;
using BLL.Rating.Objects;

namespace BLL.Social.Journals.Objects
{
    public class JournalPreviewVm
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public AuthorVm Author { get; set; }

        public RatingInfo Likes { get; set; }
        
        public DateTime Created { get; set; }
    }
}