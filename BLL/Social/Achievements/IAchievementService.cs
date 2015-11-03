using System;
using System.Collections.Generic;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using BLL.Rating.Objects;
using BLL.Social.Achievements.Impls;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.Objects;
using DAL;
using DAL.DomainModel.Achievement;

namespace BLL.Social.Achievements
{
    public interface IAchievementService
    {
        AchievementTempVm FirstStep();
        ServiceResult<CreateAchievementResult> CreateOrUpdateAchievement(AchievementCreateVm model);
        IEnumerable<string> GetFilter();
        AchievementDisplayVm GetAchivement(int id);
        PagedListVm<AchievementPreviewVm> GetStartedAchivements(AchievementSearch search);
        ServiceResult<AchievmentVoiceVm> Vote(AchievementVoteVm vote);
        ServiceResult DeleteTemp();
    }

    public class AchievementDisplayVm: AchievementPreviewVm
    {
        public string TypeImage { get; set; }
        public string VideoUrl { get; set; }
        public RatingInfo Likes { get; set; }
        public CommentsVm Comments { get; set; }
        public AchivStatus Status { get; set; }
        public string TimeLeftString { get; set; }   
    }

    public enum AchivStatus
    {
        Fail,
        Credit
    }
}