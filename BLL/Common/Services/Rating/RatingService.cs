using System;
using System.Web.Mvc;
using AutoMapper.Internal;
using BLL.Common.Objects;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;

namespace BLL.Common.Services.Rating
{
    class RatingService : IRatingService
    {
        private readonly IRepository _repository;

        public RatingService(IRepository repository)
        {
            _repository = repository;
        }

        public ServiceResult Rate<TEntity, TRatingEntity>(int entityId, RatingType ratingType)
            where TEntity : class, IHasRating<TEntity>
            where TRatingEntity : class, IRatingEntity<TEntity>
        {
            var entity = _repository.Find<TEntity>(entityId);
            if (entity != null)
            {
                var ratingEntity = Activator.CreateInstance<TRatingEntity>();
                ratingEntity.RatedEntity = entity;
                ratingEntity.RatingType = ratingType;
                _repository.Add(ratingEntity);
                _repository.SaveChanges();
                return new ServiceResult() {Success = true};
            }
            return new ServiceResult() {Success = false};
        }
    }
}