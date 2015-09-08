using System;
using System.Collections.Generic;
using BLL.Blog.ViewModels;
using BLL.Rating.Objects;
using BLL.Social.Journals.Objects;

namespace BLL.Comments.Objects
{
    public class CommentSocial
    {
        public string Text { get; set; }

        public int Id { get; set; }

        public CommentFor CommentFor { get; set; }

        public AuthorVm Author { get; set; }

        public RatingInfo Likes { get; set; }

        public DateTime Date { get; set; }
    }

    public class CommentsVm
    {
        public int Count { get; set; }

        public IEnumerable<CommentSocial> List { get; set; }
    }
}