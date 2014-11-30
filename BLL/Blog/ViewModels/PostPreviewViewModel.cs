using System;
using BLL.Blog.Enums;
using BLL.Common.Objects;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.ViewModels
{
    public class PostPreviewViewModel : HasDate, IItemInfo, IRatingInfo
    {
        public PostPreviewViewModel()
        {
            RatingEntityType = RatingEntityType.Article;
            IsDisiked = false;
            IsLiked = false;
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public Images Images { get; set; }

        public int Rating { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisiked { get; set; }

        public string AuthorAvatar { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public RatingEntityType RatingEntityType { get; set; }

        public int CommentsCount { get; set; }

        public BlogPostStatus Status { get; set; }
    }

    public interface IRatingInfo
    {
        int Id { get; set; }

        int Rating { get; set; }

        bool IsLiked { get; set; }

        bool IsDisiked { get; set; }

        RatingEntityType RatingEntityType { get; set; }
    }

    public interface IItemInfo
    {
        string AuthorAvatar { get; set; }

        int AuthorId { get; set; }

        string AuthorName { get; set; }

        int CommentsCount { get; set; }

        string Date { get; set; }
    }
}