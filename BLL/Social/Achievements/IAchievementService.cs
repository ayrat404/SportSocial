using BLL.Common.Objects;
using BLL.Social.Achievements.Objects;

namespace BLL.Social.Achievements
{
    public interface IAchievementService
    {
        AchievementTempVm FirstStep();
        ServiceResult CreateOrUpdateAchievement(AchievementCreateVm model);
    }
}