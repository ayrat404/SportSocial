using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.ConferenceEntities
{
    public class ConferenceCommentRating: IEntity, IRatingEntity<ConferenceComment>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public RatingType RatingType { get; set; }
        public int RatedEntityId { get; set; }

        [ForeignKey("RatedEntityId")]
        public virtual ConferenceComment RatedEntity { get; set; }
        public virtual AppUser User { get; set; }
    }
}