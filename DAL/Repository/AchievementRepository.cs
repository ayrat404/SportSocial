using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.Achievement.Objects;
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
                .Include(a => a.AchievementType)
                .SingleOrDefault(a => a.UserId == userId && a.Status == AchievementStatus.InCreating);
        }

        public List<AchievementType> GetTypes()
        {
            return Queryable<AchievementType>()
                .ToList();
        }

        public Achievement GetAchievement(int id)
        {
            return Queryable<Achievement>()
                .Include(a => a.AchievementMedia)
                .Include(a => a.Comments)
                .Include(a => a.Comments.Select(c => c.RatingEntites))
                .Include(a => a.RatingEntites)
                .Include(a => a.User)
                .Include(a => a.AchievementType)
                .Single(a => a.Id == id);
        }

        public List<Achievement> GetThreeRandomAchievements()
        {
            return Queryable<Achievement>()
                .Where(a => a.Status == AchievementStatus.Started)
                .Include(a => a.RatingEntites)
                .Include(a => a.User)
                .Include(a => a.AchievementType)
                .OrderBy(a => SqlFunctions.Rand(1))
                .Take(3)
                .ToList();
        }
    }
}