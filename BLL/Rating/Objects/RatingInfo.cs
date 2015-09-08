using System.Collections.Generic;
using BLL.Blog.ViewModels;
using BLL.Social.Journals.Objects;

namespace BLL.Rating.Objects
{
    public class RatingInfo
    {
        public int Count { get; set; }

        public bool IsLiked { get; set; }

        public List<AuthorVm> List { get; set; }
    }
}