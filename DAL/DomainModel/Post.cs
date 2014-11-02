using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Base;
using DAL.DomainModel.EnumProperties;

namespace DAL.DomainModel
{
    public class Post: IEntity, IAuditable, ICultrureSpecific, IDeletable
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int RubricId { get; set; }

        public string Lang { get; set; }

        public string ImageUrl { get; set; }

        public BlogPostStatus Status { get; set; }

        public string CancelMessage { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Rubric Rubric { get; set; }
        public virtual AppUser User { get; set; }
        public bool Deleted { get; set; }
    }
}