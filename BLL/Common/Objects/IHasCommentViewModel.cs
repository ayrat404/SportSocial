using BLL.Comments.Objects;

namespace BLL.Common.Objects
{
    public interface IHasCommentViewModel
    {
        int Id { get; set; }
        Comment[] Comments { get; set; }
        int CommentsCount { get; set; }
        CommentItemType ItemType { get; set; }
    }

    public enum CommentItemType
    {
        Article,
        Conference
    }
}