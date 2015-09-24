using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.Achievement
{
    public class AchievementTypeValue: IEntity
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string CupImage { get; set; }

        public string Value { get; set; }

        public AchievementType Type { get; set; }
    }
}