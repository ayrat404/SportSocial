﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.Objects;
using BLL.Social.Tags;
using BLL.Social.Tape;
using BLL.Social.UserProfile;
using BLL.Storage;
using BLL.Storage.Impls;
using BLL.Storage.Objects.Enums;
using DAL;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.Achievement.Objects;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Social.Achievements.Impls
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly ICurrentUser _currentUser;
        private readonly ITapeService _tapeService;
        private readonly IMediaService _mediaService;

        private const int DurationDays = 4;
        private const int VoteCount = 3;
        
        public AchievementService(IAchievementRepository achievementRepository, ICurrentUser currentUser, ITapeService tapeService, IMediaService mediaService)
        {
            _achievementRepository = achievementRepository;
            _currentUser = currentUser;
            _tapeService = tapeService;
            _mediaService = mediaService;
        }

        public AchievementTempVm FirstStep()
        {
            var model = new AchievementTempVm();
            var tempAchievement = _achievementRepository.GetTempAchievement(_currentUser.UserId);
            model.Model = tempAchievement == null ? null : tempAchievement.MapTo<AchievementCreateVm>();
            model.Cards = _achievementRepository.GetTypes().MapEachTo<AchievementTypeVm>();
            model.Marks = GetAchievementsToVote();
            return model;
        }

        

        public ServiceResult<CreateAchievementResult> CreateOrUpdateAchievement(AchievementCreateVm model)
        {
            var ach = _achievementRepository.GetTempAchievement(_currentUser.UserId);
            AddOrUpdateAchievement(ach, model);
            if (model.HasVideo())
            {
                _mediaService.AttachMediaToEntity(new List<MediaVm>() {model.Video}, ach.Id, UploadType.Achievement);
                if (CheckAndCompleteAchievement(ach))
                {
                    var createResult = new CreateAchievementResult
                    {
                        Id = ach.Id,
                        IsPublished = true
                    };
                    return ServiceResult.SuccessResult(createResult);
                    //return ServiceResult.SuccessResult("Заявка достижение создана".Resource(this));
                }
            }
            return ServiceResult.SuccessResult<CreateAchievementResult>(null);
        }

        public IEnumerable<string> GetFilter()
        {
            return _achievementRepository.GetTypes().Select(r => r.Title);
        }

        public AchievementDisplayVm GetAchivement(int id)
        {
            var ach = _achievementRepository.GetAchievement(id);
            return ach.MapTo<AchievementDisplayVm>();
        }

        public PagedListVm<AchievementPreviewVm> GetStartedAchivements(AchievementSearch search)
        {
            int skip = search.Count*search.Page - search.Count;
            var achDto = _achievementRepository.GetAhievements(search.Status, search.Actual, search.Type, skip, search.Count);
            return new PagedListVm<AchievementPreviewVm>
            {
                List = achDto.List.MapEachTo<AchievementPreviewVm>(),
                IsMore = search.Count*search.Page < achDto.Count
            };
        }

        public ServiceResult<AchievmentVoiceVm> Vote(AchievementVoteVm vote)
        {
            var ach = _achievementRepository.GetAchievementForVote(vote.Id);
            if (ach.UserId == _currentUser.UserId)
            {
                return ServiceResult.ErrorResult<AchievmentVoiceVm>("Вы не можете голосовать за свою заявку".Resource(this));
            }
            if (ach.UserAlreadyVoted(_currentUser.UserId))
            {
                return ServiceResult.ErrorResult<AchievmentVoiceVm>("Вы уже проголосовали за данную заявку".Resource(this));
            }
            var status = ach.GetStatus();
            if (status == AchievementStatus.Fail || status == AchievementStatus.Credit)
            {
                return ServiceResult.ErrorResult<AchievmentVoiceVm>("");
            }
            var voice = new AchievementVoice
            {
                AchievementId = ach.Id,
                VoteFor = vote.Action == VoteType.Like,
                UserId = _currentUser.UserId
            };
            _achievementRepository.Add(voice);
            _currentUser.User.Profile.AchievementVoiceCount++;
            _achievementRepository.Update(_currentUser.User.Profile);
            ach.ReCalculateVoteRatio();
            _achievementRepository.SaveChanges();
            var voiceVm = ach.MapTo<AchievmentVoiceVm>();
            var result = ServiceResult.SuccessResult(voiceVm);
            var tempAchievement = _achievementRepository.GetTempAchievement(_currentUser.UserId);
            if (tempAchievement != null)
            {
                if (CheckAndCompleteAchievement(tempAchievement))
                {
                    result.SuccessMessage = "Заявка на достижение создана".Resource(this);
                }
            }
            return result;
        }

        public ServiceResult DeleteTemp()
        {
            var ach = _achievementRepository.GetTempAchievement(_currentUser.UserId);
            if (ach != null)
            {
                _achievementRepository.Delete(ach);
                _achievementRepository.SaveChanges();
            }
            return ServiceResult.SuccessResult();
        }

        private List<AchievementPreviewVm> GetAchievementsToVote()
        {
            int count = (VoteCount - _currentUser.User.Profile.AchievementVoiceCount) % 4;
            count = count > 0 ? count : 0;
            var achToVote =  count > 0 ? _achievementRepository.GetRandomAchievements(_currentUser.UserId, count)
                                                                   .MapEachTo<AchievementPreviewVm>().ToList()
                                           : new List<AchievementPreviewVm>();
            //if (achToVote.Count <= count)
            //{
            //    return new List<AchievementPreviewVm>();
            //}
            return achToVote;
        }

        private void AddOrUpdateAchievement(Achievement ach, AchievementCreateVm model)
        {
            bool isNew = false;
            if (ach == null)
            {
                ach = new Achievement();
                isNew = true;
            }
            ach.Step = model.HasVideo() ? 2 : 1;
            ach.Status = AchievementStatus.InCreating;
            ach.UserId = _currentUser.UserId;
            ach.TypeId = model.Type.Id;
            ach.DurationDays = DurationDays;
            var typeValue = _achievementRepository
                .Queryable<AchievementTypeValue>()
                .Single(v => v.TypeId == model.Type.Id 
                          && v.Value == model.Type.Value);
            ach.Value = typeValue;
            if (isNew)
                _achievementRepository.Add(ach);
            else
                _achievementRepository.Update(ach);
            _achievementRepository.SaveChanges();
        }

        private bool CheckAndCompleteAchievement(Achievement ach)
        {
            if (ach.Step != 2)
            {
                return false;
            }
            var achievementToVoice = GetAchievementsToVote();
            if (achievementToVoice.Count == 0)//<= _currentUser.User.Profile.AchievementVoiceCount)
            {
                ach.Status = AchievementStatus.Started;
                ach.Started = DateTime.Now;
                int updatedCount = _currentUser.User.Profile.AchievementVoiceCount - 3;
                _currentUser.User.Profile.AchievementVoiceCount = updatedCount > 0 ? updatedCount : 0;
                _achievementRepository.Update(_currentUser.User.Profile);
                _tapeService.AddToTape(ach.Id, TapeType.Achievement);
                _achievementRepository.SaveChanges();
                return true;
            }
            return false;
        }
    }

    public class CreateAchievementResult
    {
        public int Id { get; set; }

        public bool IsPublished { get; set; }
    }
}