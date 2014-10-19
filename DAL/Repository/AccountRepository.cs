using DAL.DomainModel;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class AccountRepository
    {
        private IRepository<AppUser> _repository;

        public AccountRepository(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        #region SMS

        #endregion
    }
}