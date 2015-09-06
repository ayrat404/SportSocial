using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DomainModel.JournalEntities;

namespace DAL.Repository.Interfaces
{
    public interface IJournalRepository: IRepository
    {
        List<Journal> GetJournals(int userId);
    }

    public class JournalRepository : Repository, IJournalRepository
    {
        public JournalRepository(EntityDbContext context) : base(context)
        {
        }

        public List<Journal> GetJournals(int userId)
        {
            return Queryable<Journal>()
                .Where(j => j.UserId == userId)
                .Include(j => j.RatingEntites)
                .Include(j => j.RatingEntites.Select(r => r.User))
                .Include(j => j.User)
                .ToList();
        }
    }
}