using System;
using BLL.Social.Journals.Objects;

namespace BLL.Social.Achievement.Objects
{
    public class AchievementPreviewVm
    {
        public int Id { get; set; }
        public string IconUrl { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public int TimeSpent { get; set; }
        public AchievmentVoice Voice { get; set; }
        public AuthorVm User { get; set; }
    }
}