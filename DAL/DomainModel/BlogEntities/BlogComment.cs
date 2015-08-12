using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.BlogEntities
{
    public class BlogComment: CommentEntity<BlogComment>, IEntity, IAuditable, IDeletable, IHasRating<BlogCommentRating>
    {
        public int TotalRating { get; set; }

        [ForeignKey("CommentedEntityId")]
        public virtual Post CommentedEntity { get; set; }

        public virtual ICollection<BlogCommentRating> RatingEntites { get; set; }
    }
}