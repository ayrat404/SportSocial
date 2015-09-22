using DAL.DomainModel.Achievement.Objects;

namespace BLL.Social.Achievements.Objects
{
    public class AchievementSearch: BaseSearch
    {
        public AchievementState Actual { get; set; }

        public AchievementStatus Status { get; set; }

        public string Type { get; set; }

    }

    public class BaseSearch
    {
        public int Count { get; set; }

        public int Page { get; set; }
    }

    public class ProfileSearch : BaseSearch
    {
        
    }
}