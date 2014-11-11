using System.Collections.Generic;
using BLL.Common.Objects;

namespace BLL.Blog.ViewModels
{
    public class PostListViewModel: IPagedViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<PostPreviewViewModel> PostPreview { get; set; }
    }
}