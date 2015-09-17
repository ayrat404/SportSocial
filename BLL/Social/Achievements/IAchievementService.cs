using BLL.Common.Services.CurrentUser;
using BLL.Social.Achievements.Objects;
using DAL.Repository.Interfaces;

namespace BLL.Social.Achievements
{
    public interface IAchievementService
    {
        AchievementTempVm FirstStep();
    }

    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly ICurrentUser _currentUser;
        
        public AchievementService(IAchievementRepository achievementRepository, ICurrentUser currentUser)
        {
            _achievementRepository = achievementRepository;
            _currentUser = currentUser;
        }

        public AchievementTempVm FirstStep()
        {
            var tempAchievement = _achievementRepository.GetTempAchievement(_currentUser.UserId);
            return null;
        }
    }
}