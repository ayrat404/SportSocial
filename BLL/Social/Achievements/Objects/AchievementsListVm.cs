using System.Collections.Generic;

namespace BLL.Social.Achievements.Objects
{
    public class AchievementsListVm
    {
        public IEnumerable<AchievementPreviewVm> List { get; set; }

        public bool IsMore { get; set; }
    }
}