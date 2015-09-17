using System.Collections.Generic;

namespace BLL.Social.Achievements.Objects
{
    public class AchievementTypeVm
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public string[] Values { get; set; }
    }
}