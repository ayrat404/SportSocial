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

    public class RatingEntity<THasRatingEntity> where THasRatingEntity: class
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public RatingType RatingType { get; set; }

        public int RatedEntityId { get; set; }

        public THasRatingEntity RatedEntity { get; set; }

        public AppUser User { get; set; }
    }
}