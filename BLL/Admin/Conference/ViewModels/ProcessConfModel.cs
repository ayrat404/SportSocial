using BLL.Comments.Objects;
using BLL.Common.Objects;

namespace BLL.Admin.Conference.ViewModels
{
    public class ProcessConfModel: ConfModel, IHasCommentViewModel
    {
        public CommentsInfo CommentsInfo { get; set; }
    }
}