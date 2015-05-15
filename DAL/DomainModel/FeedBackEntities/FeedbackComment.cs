using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.FeedBackEntities
{
    public class FeedbackComment: IEntity, IAuditable, ICommentEntity<Feedback>, IHasRating<FeedbackCommentRating>
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public int? CommentForId { get; set; }

        public int CommentedEntityId { get; set; }

        public int TotalRating { get; set; }

        public bool ByFortress { get; set; }

        [ForeignKey("CommentedEntityId")]
        public virtual Feedback CommentedEntity { get; set; }

        [ForeignKey("CommentForId")]
        public FeedbackComment CommentFor { get; set; }

        public virtual AppUser User { get; set; }

        public virtual ICollection<FeedbackCommentRating> RatingEntites { get; set; }
    }
}