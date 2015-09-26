using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.Achievement.Objects;
using DAL.DomainModel.Interfaces;
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
                .Include(a => a.Value)
                .Include(a => a.AchievementType)
                .SingleOrDefault(a => a.UserId == userId && a.Status == AchievementStatus.InCreating);
        }

        public List<AchievementType> GetTypes()
        {
            return Queryable<AchievementType>()
                .Include(t => t.Values)
                .ToList();
        }

        public Achievement GetAchievementForVote(int id, int userId)
        {
            return Queryable<Achievement>()
                .Include(a => a.Voices)
                .SingleOrDefault(a => a.Id == id 
                                   && a.UserId != userId
                                   && a.Voices.All(v => v.UserId != userId));
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
                .Include(a => a.Value)
                .Single(a => a.Id == id);
        }

        public ListDto<Achievement> GetAhievements(AchievementStatus status, AchievementState state, string type, int skip, int take)
        {
            IQueryable<Achievement> query = Queryable<Achievement>()
                .Include(a => a.Voices)
                .Include(a => a.User)
                .Include(a => a.AchievementType)
                .Include(a => a.Value);
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
            return new ListDto<Achievement>
            {
                Count = query.Count(),
                List = query.OrderByDescending(a => a.Id)
                    .Skip(skip)
                    .Take(take)
                    .ToList()
            };
        }

        public List<Achievement> GetRandomAchievements(int userId, int count)
        {
            return Queryable<Achievement>()
                .Where(a => a.Status == AchievementStatus.Started)
                .Where(a => a.UserId != userId && a.Voices.All(v => v.UserId != userId))
                .Include(a => a.Voices)
                .Include(a => a.User)
                .Include(a => a.Value)
                .OrderBy(a => SqlFunctions.Rand(1))
                .Take(count)
                .ToList();
        }
    }

    public class ListDto<T> where T : IEntity
    {
        public int Count { get; set; }
        public List<T> List { get; set; }
    }
}