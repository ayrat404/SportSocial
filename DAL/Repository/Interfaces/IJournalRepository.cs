namespace DAL.Repository.Interfaces
{
    public interface IJournalRepository: IRepository
    {
         
    }

    public class JournalRepository : Repository, IJournalRepository
    {
        public JournalRepository(EntityDbContext context) : base(context)
        {
        }
    }
}