using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel
{
    public class Profile: ICultrureSpecific
    {
        [ForeignKey("AppUser")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Lang { get; set; }

        public string Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Sex Sex { get; set; }

        public int AchievementVoiceCount { get; set; }

        public SportExperience Experience { get; set; }

        public virtual AppUser AppUser { get; set; }

        public bool IsPaid { get; set; }
        public int ReadedNews { get; set; }

        public int GetAge()
        {
            return new DateTime((DateTime.Now - BirthDate).Ticks).Year;
        }
    }
}