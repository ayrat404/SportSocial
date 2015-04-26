using System;
using BLL.Blog.Enums;
using BLL.Blog.ViewModels.Base;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using BLL.Rating.Enums;

namespace BLL.Blog.ViewModels
{
    public class PostDisplayModel: PostBaseModel, IHasCommentViewModel, IItemInfo, IRatingInfo
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string AuthorAvatar { get; set; }

        public int MoreCommentsCount { get; set; }

        public int TotalCommentsCount { get; set; }

        public CommentItemType ItemType { get; set; }

        public Comment[] Comments { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisiked { get; set; }

        public RatingEntityType RatingEntityType { get; set; }

        public string Date { get; set; }

        public string RubricTitle { get; set; }
    }
}