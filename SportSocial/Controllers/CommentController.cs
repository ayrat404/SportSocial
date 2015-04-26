using System;
using System.Web.Mvc;
using BLL.Comments;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.ConferenceEntities;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class CommentsController: SportSocialControllerBase
    {

        [HttpPost]
        [CustomAntiForgeryValidator]
        public JsonResult Comment(CreateCommentViewModel createComment)
        {
            if (ModelState.IsValid)
            {
                var comment = CreateGenericService(createComment.ItemType).AddComment(createComment);
                return Json(new { Success = true, Comment = comment });
            }
            return Json(new {Success = false});
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult LoadComments(int id, CommentItemType itemType)
        {
            var comments = CreateGenericService(itemType).LoadComments(id, itemType);
            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        private ICommentServiceMethods CreateGenericService(CommentItemType type)
        {
            Type ratingServiseType = typeof (ICommentService<,>)
                .MakeGenericType(GetComentedEntityByType(type), GetCommentEntityByType(type));
            return (ICommentServiceMethods)DependencyResolver.Current.GetService(ratingServiseType);
        }

        private Type GetComentedEntityByType(CommentItemType type)
        {
            switch (type)
            {
                case CommentItemType.Article:
                    return typeof (Post);
                case CommentItemType.Conference:
                    return typeof (Conference);
            }
            return null;
        }

        private Type GetCommentEntityByType(CommentItemType type)
        {
            switch (type)
            {
                case CommentItemType.Article:
                    return typeof (BlogComment);
                case CommentItemType.Conference:
                    return typeof(ConferenceComment);
            }
            return null;
        }

    }
}