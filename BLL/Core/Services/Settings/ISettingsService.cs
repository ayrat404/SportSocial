using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Common.Extensions;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Core.Services.Settings.Objects;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;

namespace BLL.Core.Services.Settings
{
    public interface ISettingsService
    {
        ServiceResult SaveSettings(ProfileSettings settings);
        ProfileSettings GetProfileSettings();
    }

    public class SettingsService : ISettingsService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository _repository;

        public SettingsService(ICurrentUser currentUser, IRepository repository)
        {
            _currentUser = currentUser;
            _repository = repository;
        }

        public ServiceResult SaveSettings(ProfileSettings settings)
        {
            var profile = _currentUser.User.Profile;
            profile.FirstName = settings.FirstName;
            profile.LastName = settings.LastName;
            profile.BirthDate = settings.Birthday;
            profile.Sex = settings.Gender.Value;
            profile.Experience = (SportExperience)settings.SportTime.Value;
            _repository.SaveChanges();
            return ServiceResult.SuccessResult();
        }

        public ProfileSettings GetProfileSettings()
        {
            var profile = _currentUser.User.Profile;
            return new ProfileSettings
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                SportTime =
                    new SportExpirienceVm()
                    {
                        Label = profile.Experience.GetDescription(),
                        Value = (int)profile.Experience
                    },
                Birthday = profile.BirthDate,
                Gender =
                    new SexVm()
                    {
                        Label = profile.Sex.GetDescription(),
                        Value = profile.Sex
                    },
            };
        }
    }
}