using System;
using BLL.Blog.Enums;
using BLL.Common.Objects;
using BLL.Rating.Enums;

namespace BLL.Blog.ViewModels
{
    public interface IRatingInfo
    {
        int Id { get; set; }

        int Rating { get; set; }

        bool IsLiked { get; set; }

        bool IsDisiked { get; set; }

        RatingEntityType RatingEntityType { get; set; }
    }

    public interface IItemInfo: IHasDate
    {
        string AuthorAvatar { get; set; }

        int AuthorId { get; set; }

        string AuthorName { get; set; }

        int TotalCommentsCount { get; set; }
    }
}