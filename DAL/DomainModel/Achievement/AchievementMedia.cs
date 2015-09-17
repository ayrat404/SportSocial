using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Base;

namespace DAL.DomainModel.Achievement
{
    [Table("AchievementMedia")]
    public class AchievementMedia : MediaBase<Achievement>
    {
       
    }
}