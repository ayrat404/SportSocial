using System;
using BLL.Blog.Enums;
using BLL.Common.Objects;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.ViewModels
{
    public class PostPreviewViewModel : IItemInfo, IRatingInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public Images Images { get; set; }

        public DateTime Date { get; set; }

        public int Rating { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisiked { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public RatingEntityType RatingEntityType { get; set; }

        public int CommentsCount { get; set; }
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
        string AuthorId { get; set; }

        string AuthorName { get; set; }

        int CommentsCount { get; set; }

        DateTime Date { get; set; }
    }
}