using System.Collections.Generic;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.JournalEntities
{
    public class JournalComment: CommentEntity<JournalComment, Journal>, IHasRating<JournalCommentRating>
    {
        public int TotalRating { get; set; }
        public ICollection<JournalCommentRating> RatingEntites { get; set; }
    }
}