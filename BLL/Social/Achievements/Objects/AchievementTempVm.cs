using System.Collections.Generic;

namespace BLL.Social.Achievements.Objects
{
    public class AchievementTempVm
    {
        public AchievementCreateVm Model { get; set; }

        public IEnumerable<AchievementTypeVm> Cards { get; set; }

        public IEnumerable<AchievementPreviewVm> Marks { get; set; }
    }
}