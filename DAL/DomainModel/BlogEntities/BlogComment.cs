using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.BlogEntities
{
    public class BlogComment: IEntity, IAuditable, IDeletable, ICommentEntity<Post>, IHasRating<BlogCommentRating>
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public bool ByFortress { get; set; }

        public int TotalRating { get; set; }
        public int UserId { get; set; }
        public int? CommentForId { get; set; }
        public int CommentedEntityId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("CommentedEntityId")]
        public virtual Post CommentedEntity { get; set; }
        [ForeignKey("CommentForId")]
        public virtual BlogComment CommentFor { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<BlogCommentRating> RatingEntites { get; set; }
    }
}