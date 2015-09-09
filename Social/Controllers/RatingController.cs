using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Rating;
using BLL.Rating.Models;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/like")]
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [Route("")]
        public ApiResult Like(RatingModel likeModel)
        {
            if (ModelState.IsValid)
            {
                return ApiResult(_ratingService.Rate(likeModel));
            }
            return ModelStateErrors();
        }
    }
}
