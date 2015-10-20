using BLL.Social.UserProfile.Objects;
using DAL.DomainModel.Achievement.Objects;
using DAL.DomainModel.EnumProperties;

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
        public Sex? Gender { get; set; }

        public SportExperience? SportTime { get; set; }

        public AgeSearch? Age { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Query { get; set; }
    }
}