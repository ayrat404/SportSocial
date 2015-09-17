using System;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.Achievement
{
    public class AchievementType: IEntity
    {
        public int Id { get; set; }

        public string ImgUrl { get; set; }

        public string Title { get; set; }

        public string Values { get; set; }

        public string[] GetValues()
        {
            return Values.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}