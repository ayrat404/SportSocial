using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
                .Include(a => a.Voices)
                .Include(a => a.User)
                .Include(a => a.AchievementType)
                .Single(a => a.Id == id);
        }

        public AchievementDto GetAhievements(AchievementStatus status, AchievementState state, string type, int skip, int take)
        {
            IQueryable<Achievement> query = Queryable<Achievement>()
                .Include(a => a.Voices)
                .Include(a => a.User)
                .Include(a => a.AchievementType);
            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(a => a.AchievementType.Title == type);
            }
            switch (state)
            {
                case AchievementState.Opened:
                    query = query.Where(a => a.Status == AchievementStatus.Started
                                             && DbFunctions.AddDays(a.Started, a.DurationDays) > DateTime.Now);
                    break;
                case AchievementState.Closed:
                    query = query.Where(a => a.Status == AchievementStatus.Started
                                             && DbFunctions.AddDays(a.Started, a.DurationDays) < DateTime.Now);
                    break;
                default:
                    query = query.Where(a => a.Status != AchievementStatus.InCreating);
                    break;
            }
            switch (status)
            {
                case AchievementStatus.Fail:
                    query = query.Where(a => ((a.Voices.Count(v => v.VoteFor)/a.Voices.Count(v => !v.VoteFor)) < 0.75));
                    break;
                case AchievementStatus.Credit:
                    query = query.Where(a => ((a.Voices.Count(v => v.VoteFor)/a.Voices.Count(v => !v.VoteFor)) >= 0.75));
                    break;
            }
            return new AchievementDto()
            {
                Count = query.Count(),
                List = query.OrderBy(a => a.Id)
                    .Skip(skip)
                    .Take(take)
                    .ToList()
            };
        }

        public List<Achievement> GetThreeRandomAchievements()
        {
            return Queryable<Achievement>()
                .Where(a => a.Status == AchievementStatus.Started)
                .Include(a => a.Voices)
                .Include(a => a.User)
                .Include(a => a.AchievementType)
                .OrderBy(a => SqlFunctions.Rand(1))
                .Take(3)
                .ToList();
        }
    }

    public class AchievementDto
    {
        public int Count { get; set; }
        public List<Achievement> List { get; set; }
    }
}