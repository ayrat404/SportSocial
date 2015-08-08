using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.ConferenceEntities
{
    public class ConferenceComment: CommentEntity<ConferenceComment>, IHasRating<ConferenceCommentRating>
    {
        public int TotalRating { get; set; }
        [ForeignKey("CommentedEntityId")]
        public virtual Conference CommentedEntity { get; set; }
        public ICollection<ConferenceCommentRating> RatingEntites { get; set; }
    }
}