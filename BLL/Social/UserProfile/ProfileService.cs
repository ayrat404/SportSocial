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
        private readonly ICurrentUser _currentUser;

        public ProfileService(IJournalService journalService, ICurrentUser currentUser, IRepository repository)
        {
            _journalService = journalService;
            _currentUser = currentUser;
            _repository = repository;
        }

        public ProfileFull GetProfileFull(int id)
        {
            var user = _repository.Queryable<AppUser>()
                .Where(u => u.Id == id)
                .Include(u => u.Profile)
                .Include(u => u.Subscribes.Select(f => f.ToUser))
                .Include(u => u.Folowers.Select(f => f.FolowerUser))
                .Single();
            //var profileFull = new ProfileFull
            //{
            //    FullName = user.FullName(),
            //    Avatar = user.Profile.Avatar,
            //    SportTime = (int) user.Profile.Experience,
            //    IsOwner = user.Id == _currentUser.UserId,
            //    Age = new DateTime((DateTime.Now - user.Profile.BirthDate).Ticks).Year,
            //    Journals = _journalService.GetJournals(id),
            //    Subscribe = new UserInfos(),
            //    Followers = new UserInfos()
            //};
            //return profileFull;
            return user.MapTo<ProfileFull>();
        }

        public ProfileListVm GetUsers(ProfileSearch search)
        {
            int skip = search.Count*search.Page - search.Count;
            var users = _repository.GetUsers(skip, search.Count);
            return new ProfileListVm
            {
                IsMore = search.Count*search.Page < users.Count,
                List = users.List.MapEachTo<ProfilePreview>()
            };
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
}