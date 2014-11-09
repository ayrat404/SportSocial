using BLL.Blog.ViewModels;

namespace BLL.Common.Objects
{
    public interface IHasCommentViewModel
    {
        int Id { get; set; }
        Comment[] Comments { get; set; }
        int CommentsCount { get; set; }
    }
}