using System;
using System.Collections.Generic;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.BlogEntities
{
    public class Post: IEntity, IAuditable, ICultrureSpecific, IDeletable, IHasRating<PostRating>, IHasComments<BlogComment>
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public bool IsFortress { get; set; }

        public int RubricId { get; set; }

        public string Lang { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public BlogPostStatus Status { get; set; }

        public string CancelMessage { get; set; }

        public int TotalRating { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }

        public virtual Rubric Rubric { get; set; }
        public virtual ICollection<BlogComment> Comments { get; set; }
        public virtual ICollection<PostRating> RatingEntites { get; set; }
        public virtual AppUser User { get; set; }
    }
}