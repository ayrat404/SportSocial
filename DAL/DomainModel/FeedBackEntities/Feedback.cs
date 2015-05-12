using System;
using System.Collections.Generic;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.FeedBackEntities
{
    public class Feedback: IEntity, IAuditable, IHasRating<FeedbackRating>, IHasComments<FeedbackComment>, IDeletable
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string  Text { get; set; }

        public string Title { get; set; }

        public int FeedbackTypeId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public int TotalRating { get; set; }

        public bool Deleted { get; set; }

        public FeedbackType FeedbackType { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<FeedbackRating> RatingEntites { get; set; }
        public virtual ICollection<FeedbackComment> Comments { get; set; }
    }
}