using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Comments;
using BLL.Comments.Objects;
using DAL.DomainModel.JournalEntities;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/comments")]
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public ApiResult AddComment(CreateCommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                return ApiResult(_commentService.AddComment(comment));
            }
            return ModelStateErrors();
        }
    }
}
