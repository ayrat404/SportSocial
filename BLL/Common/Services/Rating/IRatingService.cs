using BLL.Common.Objects;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace BLL.Common.Services.Rating
{
    public interface IRatingService
    {
        ServiceResult Rate<TEntity, TRatingEntity>(int entityId, RatingType ratingType)
            where TEntity : class, IHasRating<TEntity>
            where TRatingEntity : class, IRatingEntity<TEntity>;
    }
}