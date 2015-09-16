using System.ComponentModel.DataAnnotations;
using BLL.Social.Journals.Objects;

namespace BLL.Social.Achievements.Objects
{
    public class AchievementCreateVm
    {
        public AchievementCreateVm()
        {
            Type = new ChoosedAchievmentType();
        }
        public int Id { get; set; }

        //[Required]
        public int Step { get; set; }

        [Required]
        public ChoosedAchievmentType Type { get; set; }

        public MediaVm Video { get; set; }
    }
}