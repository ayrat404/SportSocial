using BLL.Social.Journals.Objects;

namespace BLL.Social.Achievement.Objects
{
    public class AchievementCreateVm
    {
        public int Id { get; set; }

        public int Step { get; set; }

        public ChoosedAchievmentType Type { get; set; }

        public MediaVm Video { get; set; }
    }
}