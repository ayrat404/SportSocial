using System;
using BLL.Social.Journals.Objects;

namespace BLL.Social.Achievements.Objects
{
    public class AchievementPreviewVm
    {
        public AchievementPreviewVm()
        {
            Voice = new AchievmentVoiceVm();
        }

        public int Id { get; set; }
        public string CupImage { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public long Timestamp { get; set; }
        public AchievmentVoiceVm Voice { get; set; }
        public AuthorVm Author { get; set; }
    }
}