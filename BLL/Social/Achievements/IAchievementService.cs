using System;
using System.Collections.Generic;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using BLL.Rating.Objects;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.Objects;
using DAL;

namespace BLL.Social.Achievements
{
    public interface IAchievementService
    {
        AchievementTempVm FirstStep();
        ServiceResult CreateOrUpdateAchievement(AchievementCreateVm model);
        IEnumerable<string> GetFilter();
        AchievementDisplayVm GetAchivement(int id);
    }

    public class AchievementDisplayVm: AchievementPreviewVm
    {
        public RatingInfo Likes { get; set; }
        public CommentsVm Comments { get; set; }
        public AchivStatus Status { get; set; }
    }

    public enum AchivStatus
    {
        Fail,
        Credit
    }
}