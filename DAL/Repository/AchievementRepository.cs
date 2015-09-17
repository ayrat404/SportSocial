using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using DAL.DomainModel.Achievement;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class AchievementRepository : Repository, IAchievementRepository
    {
        public AchievementRepository(EntityDbContext context) : base(context)
        {
        }

        public Achievement GetTempAchievement(int userId)
        {
            return Queryable<Achievement>()
                .Include(a => a.AchievementMedia)
                .Include(a => a.User)
                .SingleOrDefault(a => a.UserId == userId && a.Status == AchievementStatus.InProgress);
        }

        public List<Achievement> GetThreeRandomAchievements()
        {
            return Queryable<Achievement>()
                .Where(a => a.Status == AchievementStatus.Created)
                .Include(a => a.AchievementRatings)
                .Include(a => a.User)
                .Include(a => a.AchievementType)
                .OrderBy(a => SqlFunctions.Rand(1))
                .Take(3)
                .ToList();
        }



    }
}