using System;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;

namespace BLL.Comments.Objects
{
    public class Comment: HasDate
    {
        public int Id { get; set; }

        public string Avatar { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public CommentFor CommentFor { get; set; }

        public RatingInfo Rating { get; set; }
    }
}