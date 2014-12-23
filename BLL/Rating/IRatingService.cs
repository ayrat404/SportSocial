using BLL.Common.Objects;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace BLL.Rating
{
    public interface IGRatingService<TEntity, TRatingEntity>
        where TEntity: class, IHasRating<TRatingEntity>
        where TRatingEntity: class, IRatingEntity<TEntity>
    {
    }

    public interface IRatingServiceImpl
    {
        ServiceResult Rate(int entityId, RatingType ratingType);
    }
}