using BLL.Blog.ViewModels;
using BLL.Feedbacks.Objects;

namespace BLL.Feedbacks
{
    public interface IFeedbackService
    {
        FeedbackPreviewModel AddFeedback(CreateFeedbackModel createModel);
        FeedbackListModel GetFeedbacks(int pageSize, FeedbackSortType sortType, int page = 1);
        void Remove(int id);
    }
}