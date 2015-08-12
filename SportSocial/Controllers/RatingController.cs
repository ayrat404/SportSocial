using System.Web.Mvc;
using BLL.Rating;
using BLL.Rating.Models;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class RatingController: SportSocialControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        [CustomAntiForgeryValidator]
        public JsonResult Rate(RatingModel rateModel)
        {
            return Json(_ratingService.Rate(rateModel));
        }
    }
}