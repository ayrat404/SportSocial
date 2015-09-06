using System;
using System.Collections.Generic;
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

    public class RatingInfo : IRatingInfo
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisiked { get; set; }

        public RatingEntityType RatingEntityType { get; set; }

        public List<RatedUser> RatedUsers { get; set; }
    }

    public class RatedUser
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
    }

    public interface IItemInfo: IHasDate
    {
        string AuthorAvatar { get; set; }

        int AuthorId { get; set; }

        string AuthorName { get; set; }

        int TotalCommentsCount { get; set; }
    }
}