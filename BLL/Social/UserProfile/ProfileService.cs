using System;
using System.Data.Entity;
using System.Linq;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using BLL.Social.Achievements.Objects;
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
        private readonly IProfileRepository _profileRepository;
        private readonly ICurrentUser _currentUser;

        public ProfileService(IJournalService journalService, ICurrentUser currentUser, IRepository repository, IProfileRepository profileRepository)
        {
            _journalService = journalService;
            _currentUser = currentUser;
            _repository = repository;
            _profileRepository = profileRepository;
        }

        public ProfileFull GetProfileFull(int id)
        {
            var user = _profileRepository.GetUserFull(id);
            return user.MapTo<ProfileFull>();
        }

        public ProfileListVm GetUsers(ProfileSearch search)
        {
            DateTime? from;
            DateTime? to;
            GetBirthFromTo(search.Age, out from, out to);
            int skip = search.Count*search.Page - search.Count;
            var users = _profileRepository.GetUsers(from, to, search.SportTime, search.Gender, search.City, search.Country, search.Query, skip, search.Count);
            return new ProfileListVm
            {
                IsMore = search.Count*search.Page < users.Count,
                List = users.List.MapEachTo<ProfilePreview>()
            };
        }

        private void GetBirthFromTo(AgeSearch? age, out DateTime? from, out DateTime? to)
        {
            from = null;
            to = null;
            if (!age.HasValue)
            {
                return;
            }
            switch (age)
            {
                case AgeSearch.One:
                    from = DateTime.Now.AddYears(-18);
                    to = DateTime.Now;
                    return;
                case AgeSearch.Two:
                    from = DateTime.Now.AddYears(-25);
                    to = DateTime.Now.AddYears(-18);
                    return;
                case AgeSearch.Three:
                    from = DateTime.Now.AddYears(-40);
                    to = DateTime.Now.AddYears(-25);
                    return;
                case AgeSearch.Four:
                    from = DateTime.Now.AddYears(-1000);
                    to = DateTime.Now.AddYears(-40);
                    return;
            }
        }

        public ServiceResult Subscribe(SubcribeModel model)
        {
            var user = _repository.Queryable<AppUser>()
                .Include(a => a.Folowers)
                .Single(a => a.Id == model.Id);
            if (model.ActionType == SubscribeActionType.Subscribe)
            {
                if (!user.Folowers.Any(f => f.FolowerUserId == _currentUser.UserId))
                {
                    var subscribe = new Subscribe
                    {
                        FolowerUserId = _currentUser.UserId,
                        ToUserId = user.Id
                    };
                    _repository.Add(subscribe);
                    _repository.SaveChanges();
                }
            }
            else
            {
                var subscribe = _repository.Queryable<Subscribe>()
                    .SingleOrDefault(s => s.FolowerUserId == _currentUser.UserId && s.ToUserId == user.Id);
                if (subscribe != null)
                {
                    _repository.Delete(subscribe);
                }
                _repository.SaveChanges();
            }
            return ServiceResult.SuccessResult();
        }
    }

    public class TapeVm
    {
        public TapeType Type { get; set; }
        public object Object { get; set; }
    }

    public enum TapeType
    {
        Record,
        Achievement
    }
}