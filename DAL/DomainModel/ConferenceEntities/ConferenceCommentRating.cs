using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.ConferenceEntities
{
    public class ConferenceCommentRating: IEntity, IRatingEntity<ConferenceComment>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public RatingType RatingType { get; set; }
        public int RatedEntityId { get; set; }

        [ForeignKey("RatedEntityId")]
        public ConferenceComment RatedEntity { get; set; }
    }
}