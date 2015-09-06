using BLL.Common.Services.CurrentUser;
using BLL.Social.Journals;
using BLL.Social.UserProfile.Objects;
using DAL.DomainModel;
using DAL.Repository.Interfaces;

namespace BLL.Social.UserProfile
{
    public class ProfileService : IProfileService
    {
        private readonly IJournalService _journalService;
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public ProfileService(IJournalService journalService, ICurrentUser currentUser, IRepository repository)
        {
            _journalService = journalService;
            _currentUser = currentUser;
            _repository = repository;
        }

        public ProfileFull GetProfileFull(int id)
        {
            var user = _repository.Find<AppUser>(id);
            var profileFull = new ProfileFull()
            {
                FullName = user.FullName(),
                Avatar = user.Profile.Avatar,
                SportTime = user.Profile.Experience,
                IsOwner = user.Id == _currentUser.UserId,
            };
            profileFull.Journals = _journalService.GetJournals(id);
            return profileFull;
        }
    }
}