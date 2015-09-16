using System.Collections.Generic;

namespace BLL.Social.Achievement.Objects
{
    public class AchievementTypeVm
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public List<string> Values { get; set; }
    }
}