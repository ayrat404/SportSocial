using System;
using BLL.Blog.Enums;
using BLL.Blog.ViewModels.Base;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using BLL.Rating;
using BLL.Rating.Enums;

namespace BLL.Blog.ViewModels
{
    public class PostDisplayModel: PostBaseModel, IHasCommentViewModel, IHasItemInfo, IHasRating
    {
        public PostDisplayModel(): base()
        {
            RatingInfo = new RatingInfo();
            CommentsInfo = new CommentsInfo();
            ItemInfo = new ItemInfo();
        }
        public int Id { get; set; }

        public RatingInfo RatingInfo { get; set; }

        public string Date { get; set; }

        public string RubricTitle { get; set; }

        public CommentsInfo CommentsInfo { get; set; }

        public ItemInfo ItemInfo { get; set; }
    }
}