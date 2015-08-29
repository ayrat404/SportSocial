using System.Collections.Generic;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.JournalEntities
{
    public class JournalCommentRating: IRatingEntity<JournalComment>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public RatingType RatingType { get; set; }
        public int RatedEntityId { get; set; }
        public JournalComment RatedEntity { get; set; }
        public AppUser User { get; set; }
    }
}