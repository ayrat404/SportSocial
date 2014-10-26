using DAL.DomainModel;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly IRepository _repository;

        public AccountRepository(IRepository repository)
        {
            _repository = repository;
        }

        #region SMS

        #endregion

        public string GetUserLanguage(object userId)
        {
            var profile = _repository.Find<AppUser>(userId).Profile;
            return profile.Lang;
        }
    }
}