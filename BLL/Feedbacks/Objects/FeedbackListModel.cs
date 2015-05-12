using System.Collections.Generic;
using BLL.Common.Objects;

namespace BLL.Feedbacks.Objects
{
    public class FeedbackListModel: IPagedViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<FeedbackPreviewModel> PostPreview { get; set; }
    }
}