using System;
using BLL.Social.Journals.Objects;

namespace BLL.Social.Achievements.Objects
{
    public class AchievementPreviewVm
    {
        public AchievementPreviewVm()
        {
            Voice = new AchievmentVoice();
        }

        public int Id { get; set; }
        public string IconUrl { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public int TimeSpent { get; set; }
        public AchievmentVoice Voice { get; set; }
        public AuthorVm User { get; set; }
    }
}