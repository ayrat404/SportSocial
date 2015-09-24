using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.Achievement
{
    public class AchievementType: IEntity
    {
        public int Id { get; set; }

        public string ImgUrl { get; set; }

        public string Title { get; set; }

        public string VideoUrl { get; set; }

        //public string Values { get; set; }

        public string[] GetValues()
        {
            return Values.Select(v => v.Value).ToArray();
        }

        public ICollection<AchievementTypeValue> Values { get; set; }
    }
}