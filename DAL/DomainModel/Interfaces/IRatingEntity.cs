using DAL.DomainModel.EnumProperties;

namespace DAL.DomainModel.Interfaces
{
    public interface IRatingEntity<THasRatingEntity>
    {
        string UserId { get; set; }
        RatingType RatingType { get; set; }
        int RatedEntityId { get; set; }
        THasRatingEntity RatedEntity { get; set; }
    }
}