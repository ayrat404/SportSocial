using System;
using System.Web.Mvc;
using BLL.Comments;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.ConferenceEntities;
using DAL.DomainModel.FeedBackEntities;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class CommentsController: SportSocialControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [CustomAntiForgeryValidator]
        public JsonResult Comment(CreateCommentViewModel createComment)
        {
            if (ModelState.IsValid)
            {
                var comment = _commentService.AddComment(createComment);
                return Json(new { Success = true, Comment = comment });
            }
            return Json(new {Success = false});
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult LoadComments(int id, CommentItemType itemType)
        {
            var comments = _commentService.LoadComments(id, itemType);
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
    }
}