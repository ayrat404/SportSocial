using BLL.Blog.ViewModels;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using BLL.Rating.Enums;

namespace BLL.Feedbacks.Objects
{
    public class FeedbackPreviewModel: CreateFeedbackModel, IHasCommentViewModel, IRatingInfo, IItemInfo, IHasDate
    {
        public FeedbackPreviewModel()
        {
            RatingEntityType = BLL.Rating.Enums.RatingEntityType.Feedback;
            IsDisiked = false;
            IsLiked = false;
        }
        public string TypeName { get; set; }

        public int Id { get; set; }

        public int Rating { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisiked { get; set; }

        public RatingEntityType RatingEntityType { get; set; }

        public Comment[] Comments { get; set; }

        public int MoreCommentsCount { get; set; }

        public CommentItemType ItemType { get; set; }

        public string Date { get; set; }

        public string AuthorAvatar { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public int TotalCommentsCount { get; set; }
    }
}