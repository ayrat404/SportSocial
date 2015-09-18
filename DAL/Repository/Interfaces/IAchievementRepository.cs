using System.Collections.Generic;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.Achievement.Objects;

namespace DAL.Repository.Interfaces
{
    public interface IAchievementRepository: IRepository
    {
        List<Achievement> GetThreeRandomAchievements();
        Achievement GetTempAchievement(int userId);
        List<AchievementType> GetTypes();
        Achievement GetAchievement(int id);
        AchievementDto GetAhievements(AchievementStatus status, AchievementState state, string type, int skip, int take);
    }
}