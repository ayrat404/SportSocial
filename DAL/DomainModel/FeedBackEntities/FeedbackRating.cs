using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.FeedBackEntities
{
    public class FeedbackRating: IEntity, IRatingEntity<Feedback>
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public RatingType RatingType { get; set; }

        public int RatedEntityId { get; set; }

        public Feedback RatedEntity { get; set; }

        public AppUser User { get; set; }
    }
}