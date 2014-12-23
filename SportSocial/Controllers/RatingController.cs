using System;
using System.Web.Helpers;
using System.Web.Mvc;
using BLL.Blog.Enums;
using BLL.Blog.ViewModels;
using BLL.Rating;
using BLL.Rating.Enums;
using BLL.Rating.Models;
using DAL.DomainModel.BlogEntities;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class RatingController: SportSocialControllerBase
    {
        [HttpPost]
        [CustomAntiForgeryValidator]
        public JsonResult Rate(RatingModel rateModel)
        {
            var type = rateModel.EntityType;
            Type ratingServiseType = typeof (IGRatingService<,>)
                .MakeGenericType(GetRatedEntity(type), GetRatingEntity(type));
            var rateService = (IRatingServiceImpl)DependencyResolver.Current.GetService(ratingServiseType);
            return Json(rateService.Rate(rateModel.Id, rateModel.ActionType));
        }

        private Type GetRatedEntity(RatingEntityType entityType)
        {
            switch (entityType)
            {
                case RatingEntityType.Article:
                    return typeof (Post);
                case RatingEntityType.ArticleComment:
                    return typeof (BlogComment);
            }
            return null;
        }

        private Type GetRatingEntity(RatingEntityType entityType)
        {
            switch (entityType)
            {
                case RatingEntityType.Article:
                    return typeof (PostRating);
                case RatingEntityType.ArticleComment:
                    return typeof (BlogCommentRating);
            }
            return null;
        }
    }
}