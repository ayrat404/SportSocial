using System;
using System.Data.Entity;
using System.Linq;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;

namespace BLL.Rating
{
    public class RatingService<TEntity, TRatingEntity> : IRatingServiceImpl, IGRatingService<TEntity, TRatingEntity>
        where TEntity: class, IHasRating<TRatingEntity>
        where TRatingEntity: class, IRatingEntity<TEntity>
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public RatingService(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public ServiceResult Rate(int entityId, RatingType ratingType)
            //where TEntity : class, IHasRating<TEntity>
            //where TRatingEntity : class, IRatingEntity<TEntity>
        {
            var result = new RateResult() {Success = true};
            var entity = _repository.Queryable<TEntity>()
                .Include(e => e.RatingEntites)
                .SingleOrDefault(e => e.Id == entityId);
            if (entity != null)
            {
                var ratingEntity = entity.RatingEntites.SingleOrDefault(r => r.UserId == _currentUser.UserId);
                if (ratingEntity != null)
                {
                    entity.TotalRating -= (int)ratingEntity.RatingType;
                    _repository.Delete(ratingEntity);
                    //result.
                }
                if (ratingEntity == null || ratingEntity.RatingType != ratingType)
                {

                    ratingEntity = Activator.CreateInstance<TRatingEntity>();
                    ratingEntity.RatedEntity = entity;
                    ratingEntity.RatingType = ratingType;
                    ratingEntity.UserId = _currentUser.UserId;
                    entity.TotalRating += (int)ratingType;
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

    public class RateResult : ServiceResult
    {
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
    }
}