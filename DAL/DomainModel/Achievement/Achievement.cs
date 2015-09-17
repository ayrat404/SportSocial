using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.Achievement
{
    public class Achievement: IEntity, IAuditable, IDeletable
    {
        public int Id { get; set; }

        public int Step { get; set; }

        public string Value { get; set; }

        public AchievementStatus Status { get; set; }

        public int TypeId { get; set; }

        public int UserId { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public bool Deleted { get; set; }

        //public int GetRemainingTime()
        //{
        //    DateTime.Now - new DateTime()
        //}

        [ForeignKey("UserId")]
        public AppUser User { get; set; }
        [ForeignKey("TypeId")]
        public AchievementType AchievementType { get; set; }
        public ICollection<AchievementRating> AchievementRatings { get; set; }
        public ICollection<AchievementMedia> AchievementMedia { get; set; }

    }
}