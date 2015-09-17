using System.Collections.Generic;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.Achievement
{
    public class AchievementComment : CommentEntity<AchievementComment, Achievement>, IHasRating<AchievementCommentRating>
    {
        public AchievementComment()
        {
            RatingEntites = new List<AchievementCommentRating>();
        }
        public int TotalRating { get; set; }
        public ICollection<AchievementCommentRating> RatingEntites { get; set; }
    }
}