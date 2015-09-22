using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using BLL.Blog.ViewModels;
using BLL.Common.Services.CurrentUser;
using BLL.Rating.Enums;
using BLL.Rating.Objects;
using BLL.Social.Journals.Objects;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace BLL.Rating
{
    public class MapperBase
    {
        protected static  T GetService<T>() where T: class
        {
            return DependencyResolver.Current.GetService<T>() ??
                    (T)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(T));
        }
    }

    public class RatingMapper: MapperBase
    {
        public static RatingInfo MapRating<T>(IHasRating<T> entity) where T : RatingEntityBase
        {
            var currentUser =  GetService<ICurrentUser>();
            var ratingInfo = new RatingInfo();
            ratingInfo.IsLiked = entity.RatingEntites.Any(r => r.UserId == currentUser.UserId && r.RatingType == RatingType.Like);
            //ratingInfo.IsDisiked = entity.RatingEntites.Any(r => r.UserId == currentUser.UserId && r.RatingType == RatingType.Like);
            ratingInfo.Count = entity.TotalRating;
            //ratingInfo.RatingEntityType = RatingEntityType.Journal;
            ratingInfo.List = entity.RatingEntites
                .OrderByDescending(r => r.Id).Take(3)
                .Select(j => new UserInfoVm
                {
                    Id = j.UserId,
                    Avatar = j.User.Profile.Avatar,
                    FullName = j.User.Profile.FirstName + " " + j.User.Profile.LastName
                }).ToList();
            return ratingInfo;
        }

        public static RatingInfo MapRatingFull<T>(IHasRating<T> entity) where T : RatingEntityBase
        {
            var ratingInfo = MapRating(entity);
            ratingInfo.List = entity.RatingEntites
                .OrderByDescending(r => r.Id).Take(3)
                .Select(j => new UserInfoVm
                {
                    Id = j.UserId,
                    Avatar = j.User.Profile.Avatar,
                    FullName = j.User.Profile.FirstName + " " + j.User.Profile.LastName
                }).ToList();
            return ratingInfo;
        }
    }
}