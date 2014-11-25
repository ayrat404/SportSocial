using System;
using BLL.Blog.Enums;
using BLL.Comments.Objects;
using BLL.Common.Objects;

namespace BLL.Blog.ViewModels
{
    public class BlogPostViewModel: HasDate, IHasCommentViewModel, IItemInfo, IRatingInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public Images[] Images { get; set; }

        public int Rating { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string AuthorAvatar { get; set; }

        public int CommentsCount { get; set; }

        public int TotalCommentsCount { get; set; }

        public CommentItemType ItemType { get; set; }

        public Comment[] Comments { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisiked { get; set; }

        public RatingEntityType RatingEntityType { get; set; }
    }
}