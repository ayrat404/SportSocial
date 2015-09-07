using DAL.DomainModel.EnumProperties;

namespace DAL.DomainModel.Interfaces
{
    public interface IRatingEntity<THasRatingEntity>: IEntity
    {
        int UserId { get; set; }
        RatingType RatingType { get; set; }
        int RatedEntityId { get; set; }
        THasRatingEntity RatedEntity { get; set; }
        AppUser User { get; set; }
    }
}