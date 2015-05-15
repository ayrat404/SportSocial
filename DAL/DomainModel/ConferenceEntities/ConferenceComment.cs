using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.ConferenceEntities
{
    public class ConferenceComment: IDeletable, IEntity, ICommentEntity<Conference>, IHasRating<ConferenceCommentRating>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TotalRating { get; set; }
        public int UserId { get; set; }
        public int? CommentForId { get; set; }
        public int CommentedEntityId { get; set; }
        public bool ByFortress { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }


        [ForeignKey("CommentForId")]
        public ConferenceComment CommentFor { get; set; }
        [ForeignKey("CommentedEntityId")]
        public virtual Conference CommentedEntity { get; set; }
        public virtual AppUser User { get; set; }
        public ICollection<ConferenceCommentRating> RatingEntites { get; set; }
    }
}