using System.Collections.Generic;
using BLL.Common.Objects;

namespace BLL.Blog.ViewModels
{
    public class PostListModel: IPagedViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<PostPreviewModel> PostPreview { get; set; }
    }
}