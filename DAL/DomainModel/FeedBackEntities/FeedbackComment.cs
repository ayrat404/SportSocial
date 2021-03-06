﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.FeedBackEntities
{
    public class FeedbackComment: CommentEntity<FeedbackComment, Feedback>, IHasRating<FeedbackCommentRating>
    {
        public int TotalRating { get; set; }
        public virtual ICollection<FeedbackCommentRating> RatingEntites { get; set; }
    }
}