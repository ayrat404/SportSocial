using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
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
            var model = new AchievementTempVm();
            var tempAchievement = _achievementRepository.GetTempAchievement(_currentUser.UserId);
            model.Model = tempAchievement.MapTo<AchievementCreateVm>();
            model.Cards = _achievementRepository.GetTypes().MapEachTo<AchievementTypeVm>();
            model.Marks = _achievementRepository.GetThreeRandomAchievements().MapEachTo<AchievementPreviewVm>();
            return model;
        }
    }
}