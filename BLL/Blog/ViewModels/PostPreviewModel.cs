using BLL.Blog.Enums;
using BLL.Blog.ViewModels.Base;
using BLL.Common.Objects;
using BLL.Rating.Enums;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.ViewModels
{
    public class PostPreviewModel : PostBaseModel, IItemInfo, IRatingInfo
    {
        public PostPreviewModel()
        {
            RatingEntityType = RatingEntityType.Article;
            IsDisiked = false;
            IsLiked = false;
        }
        public int Id { get; set; }

        public int Rating { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisiked { get; set; }

        public string AuthorAvatar { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public RatingEntityType RatingEntityType { get; set; }

        public int TotalCommentsCount { get; set; }

        public BlogPostStatus Status { get; set; }

        public string Date { get; set; }
    }
}