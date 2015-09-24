using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using DAL.DomainModel.JournalEntities;

namespace DAL.Repository.Interfaces
{
    public interface IJournalRepository: IRepository
    {
        ListDto<Journal> GetJournals(int userId, int skip, int take);
        Journal GetJournal(int journalId);
        Journal JournalForEdit(int journalId);
        List<JournalMedia> GetRandomMedia(int userId);
    }

    public class JournalRepository : Repository, IJournalRepository
    {
        public JournalRepository(EntityDbContext context) : base(context)
        {
        }

        public ListDto<Journal> GetJournals(int userId, int skip, int take)
        {
            int count = Queryable<Journal>()
                .Count(j => j.UserId == userId);
            return new ListDto<Journal>
            {
                Count = count,
                List = Queryable<Journal>()
                    .Where(j => j.UserId == userId)
                    .Include(j => j.RatingEntites)
                    .Include(j => j.RatingEntites.Select(r => r.User))
                    .Include(j => j.User)
                    .Include(j => j.Media)
                    .Include(j => j.Tags)
                    .Include(j => j.Tags.Select(t => t.Tag))
                    .Include(j => j.Media.Select(m => m.RatingEntites))
                    .OrderByDescending(j => j.Id)
                    .ToList()
            };
        }

        public Journal GetJournal(int journalId)
        {
            return Queryable<Journal>()
                .Where(j => j.Id == journalId)
                .Include(j => j.RatingEntites)
                .Include(j => j.Media)
                .Include(j => j.Media.Select(m => m.RatingEntites))
                .Include(j => j.Comments)
                .Include(j => j.Tags)
                .Include(j => j.Tags.Select(t => t.Tag))
                .Include(j => j.RatingEntites.Select(r => r.User))
                .Include(j => j.User)
                .Single();
        }

        public Journal JournalForEdit(int journalId)
        {
            return Queryable<Journal>()
                .Where(j => j.Id == journalId)
                .Include(j => j.User)
                .Include(j => j.Media)
                .Include(j => j.Tags)
                .Include(j => j.Tags.Select(t => t.Tag))
                .Single();
        }

        public List<JournalMedia> GetRandomMedia(int userId)
        {
            return Queryable<JournalMedia>()
                .Where(a => a.UserId == userId && a.EntityId != null)
                .OrderBy(a => SqlFunctions.Rand(1))
                .Take(9)
                .ToList();
        }
    }
}