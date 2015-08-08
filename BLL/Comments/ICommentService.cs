using System.Collections.Generic;
using System.Web.Mvc;
using BLL.Comments.Objects;
using BLL.Common.Objects;

namespace BLL.Comments
{
    public interface ICommentService
    {
        Comment AddComment(CreateCommentViewModel createCommentViewModel);
        IEnumerable<Comment> LoadComments(int itemId, CommentItemType itemType);
    }
}