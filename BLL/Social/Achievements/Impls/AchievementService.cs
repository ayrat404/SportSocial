using System;
using System.Collections.Generic;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.Objects;
using BLL.Storage;
using BLL.Storage.Impls.Enums;
using DAL;
using DAL.DomainModel.Achievement;
using DAL.Repository.Interfaces;

namespace BLL.Social.Achievements.Impls
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IVideoService _videoService;

        private const int DurationDays = 6;
        
        public AchievementService(IAchievementRepository achievementRepository, ICurrentUser currentUser, IVideoService videoService)
        {
            _achievementRepository = achievementRepository;
            _currentUser = currentUser;
            _videoService = videoService;
        }

        public AchievementTempVm FirstStep()
        {
            var model = new AchievementTempVm();
            var tempAchievement = _achievementRepository.GetTempAchievement(_currentUser.UserId);
            model.Model = tempAchievement == null ? null : tempAchievement.MapTo<AchievementCreateVm>();
            model.Cards = _achievementRepository.GetTypes().MapEachTo<AchievementTypeVm>();
            model.Marks = _achievementRepository.GetThreeRandomAchievements().MapEachTo<AchievementPreviewVm>();
            return model;
        }

        public ServiceResult CreateOrUpdateAchievement(AchievementCreateVm model)
        {
            var ach = _achievementRepository.GetTempAchievement(_currentUser.UserId) ?? new Achievement();
            ach.Step = model.HasVideo() ? 2 : 1;
            ach.Status = AchievementStatus.InCreating;
            ach.UserId = _currentUser.UserId;
            ach.TypeId = model.Type.Id;
            ach.Value = model.Type.Value.ToString();
            ach.DurationDays = DurationDays;
            _achievementRepository.Add(ach);
            _achievementRepository.SaveChanges();
            if (model.HasVideo())
            {
                _videoService.AttachVideosToEntity(new List<MediaVm>() {model.Video}, ach.Id, UploadType.Achievement);
                CheckAndCompleteAchievement(ach);
            }
            return ServiceResult.SuccessResult();
        }

        private void CheckAndCompleteAchievement(Achievement ach)
        {
            var achievementToVoice = _achievementRepository.GetThreeRandomAchievements();
            if (achievementToVoice.Count <= _currentUser.User.Profile.AchievementVoiceCount)
            {
                ach.Status = AchievementStatus.Started;
                ach.Started = DateTime.Now;
                _currentUser.User.Profile.AchievementVoiceCount -= achievementToVoice.Count;
                _achievementRepository.Update(_currentUser.User.Profile);
                _achievementRepository.SaveChanges();
            }
        }
    }
}