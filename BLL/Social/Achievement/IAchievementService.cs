using System;
using BLL.Social.Achievement.Objects;

namespace BLL.Social.Achievement
{
    public interface IAchievementService
    {
        AchievementTempVm FirstStep();
    }

    public class AchievementService : IAchievementService
    {
        public AchievementTempVm FirstStep()
        {
            throw new NotImplementedException();
        }
    }
}