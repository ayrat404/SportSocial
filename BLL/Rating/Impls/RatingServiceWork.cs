using System;
using System.Data.Entity;
using System.Linq;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Rating.Models;
using BLL.Rating.Objects;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;

namespace BLL.Rating.Impls
{
    internal class RatingServiceWork<TEntity, TRatingEntity> : IRatingService
        where TEntity: class, IHasRating<TRatingEntity>
        where TRatingEntity : RatingEntity<TEntity>
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public RatingServiceWork(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public ServiceResult Rate(RatingModel rateModel)
            //where TEntity : class, IHasRating<TEntity>
            //where TRatingEntity : class, IRatingEntity<TEntity>
        {
            var result = new RateResult() {Success = true};
            var entity = _repository.Queryable<TEntity>()
                .Include(e => e.RatingEntites)
                .SingleOrDefault(e => e.Id == rateModel.Id);
            if (entity != null)
            {
                var ratingEntity = entity.RatingEntites.SingleOrDefault(r => r.UserId == _currentUser.UserId);
                if (ratingEntity != null)
                {
                    entity.TotalRating -= (int)ratingEntity.RatingType;
                    _repository.Delete(ratingEntity);
                    //result.
                }
                if (ratingEntity == null || ratingEntity.RatingType != rateModel.ActionType)
                {

                    ratingEntity = Activator.CreateInstance<TRatingEntity>();
                    ratingEntity.RatedEntity = entity;
                    ratingEntity.RatingType = rateModel.ActionType;
                    ratingEntity.UserId = _currentUser.UserId;
                    entity.TotalRating += (int)rateModel.ActionType;
                    _repository.Add(ratingEntity);
                    _repository.Update(entity);
                }
                _repository.SaveChanges();
                var ratingEntities = entity.RatingEntites.ToList();
                result.LikesCount = ratingEntities.Count(r => r.RatingType == RatingType.Like);
                result.DislikesCount = ratingEntities.Count(r => r.RatingType == RatingType.Dislike);

                return result;
            }
            return new ServiceResult() {Success = false};
        }
    }
}