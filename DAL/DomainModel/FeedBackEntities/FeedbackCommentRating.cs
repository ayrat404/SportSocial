using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.FeedBackEntities
{
    public class FeedbackCommentRating: IEntity, IRatingEntity<FeedbackComment>
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public RatingType RatingType { get; set; }

        public int RatedEntityId { get; set; }

        [ForeignKey("RatedEntityId")]
        public virtual FeedbackComment RatedEntity { get; set; }

        public virtual AppUser User { get; set; }
    }
}