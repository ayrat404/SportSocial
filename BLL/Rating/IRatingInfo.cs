using BLL.Rating.Enums;

namespace BLL.Rating
{
    public interface IHasRating
    {
        int Id { get; set; }

        RatingInfo RatingInfo { get; set; }
    }

    public class RatingInfo
    {
        public int Rating { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisiked { get; set; }

        public RatingEntityType RatingEntityType { get; set; }
    }
}