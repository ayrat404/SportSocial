using System;
using System.Collections.Generic;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.BlogEntities
{
    public class BlogComment: IEntity, IAuditable, IDeletable, IHasRating<BlogComment>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TotalRating { get; set; }
        public string UserId { get; set; }
        public int CommentForId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }

        public virtual BlogComment CommentFor { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<IRatingEntity<BlogComment>> RatingEntites { get; set; }
    }
}