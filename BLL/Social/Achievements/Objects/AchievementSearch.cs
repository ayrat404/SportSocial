using DAL.DomainModel.Achievement.Objects;

namespace BLL.Social.Achievements.Objects
{
    public class AchievementSearch
    {
        public AchievementState Actual { get; set; }

        public AchievementStatus Status { get; set; }

        public string Type { get; set; }

        public int Count { get; set; }

        public int Page { get; set; }
    }
}