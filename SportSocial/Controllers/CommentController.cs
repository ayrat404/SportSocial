using System.Web.Mvc;
using BLL.Comments;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    public class CommentsController: SportSocialControllerBase
    {
        //private readonly ICommentService _commentService;

        //public CommentsController(ICommentService commentService)
        //{
        //    _commentService = commentService;
        //}

        [HttpPost]
        public JsonResult Comment(CreateCommentViewModel createCommentViewModelModel)
        {
            if (ModelState.IsValid)
            {
                //var comment = _commentService.AddComment(createCommentViewModelModel);
                //return Json(new {Success = true, Comment = comment});
            }
            return Json(new {Success = false});
        }

        [HttpGet]
        public JsonResult LoadComments(int id, CommentItemType? itemType)
        {
            //return Json(_commentService.LoadComments(id, CommentItemType.Article), JsonRequestBehavior.AllowGet);
            return null;
        }
    }
}