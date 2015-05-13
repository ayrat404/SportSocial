using System.CodeDom;
using System.Collections.Generic;
using BLL.Common.Objects;

namespace BLL.Feedbacks.Objects
{
    public class FeedbackListModel: IPagedViewModel
    {
        public FeedbackListModel()
        {
            PageInfo = new PageInfo();
        }
        public PageInfo PageInfo { get; set; }
        public IEnumerable<FeedbackPreviewModel> FeedbackPreview { get; set; }
    }
}