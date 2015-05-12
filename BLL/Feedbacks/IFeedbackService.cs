using BLL.Blog.ViewModels;
using BLL.Feedbacks.Objects;

namespace BLL.Feedbacks
{
    public interface IFeedbackService
    {
        void AddFeedback(CreateFeedbackModel createModel);
        FeedbackListModel GetFeedbacks(int pageSize, FeedbackSortType sortType, int page = 1);
    }
}