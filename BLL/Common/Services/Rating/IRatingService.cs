using BLL.Common.Objects;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace BLL.Common.Services.Rating
{
    public interface IGRatingService<TEntity, TRatingEntity>
        where TEntity: class, IHasRating<TEntity>
        where TRatingEntity: class, IRatingEntity<TEntity>
    {
        ServiceResult Rate(int entityId, RatingType ratingType);
    }

    public interface IRatingServiceImpl
    {
        ServiceResult Rate(int entityId, RatingType ratingType);
    }
}