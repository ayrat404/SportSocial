using BLL.Blog.Enums;
using BLL.Blog.ViewModels.Base;
using BLL.Common.Objects;
using BLL.Rating;
using BLL.Rating.Enums;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.ViewModels
{
    public class PostPreviewModel : PostBaseModel, IHasItemInfo, IHasRating
    {
        public PostPreviewModel()
        {
            RatingInfo = new RatingInfo
            {
                RatingEntityType = RatingEntityType.Article,
            };
            ItemInfo = new ItemInfo();
        }
        public int Id { get; set; }

        public BlogPostStatus Status { get; set; }

        public string Date { get; set; }

        public RatingInfo RatingInfo { get; set; }

        public ItemInfo ItemInfo { get; set; }
    }
}