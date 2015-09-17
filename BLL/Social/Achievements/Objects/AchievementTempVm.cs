using System.Collections.Generic;

namespace BLL.Social.Achievements.Objects
{
    public class AchievementTempVm
    {
        public AchievementCreateVm Model { get; set; }

        public List<AchievementTypeVm> Cards { get; set; }

        public List<AchievementPreviewVm> Marks { get; set; }
    }
}