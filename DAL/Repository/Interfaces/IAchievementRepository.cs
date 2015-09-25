using System.Collections.Generic;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.Achievement.Objects;

namespace DAL.Repository.Interfaces
{
    public interface IAchievementRepository: IRepository
    {
        List<Achievement> GetRandomAchievements(int userId, int count);
        Achievement GetTempAchievement(int userId);
        List<AchievementType> GetTypes();
        Achievement GetAchievementForVote(int id, int userId);
        Achievement GetAchievement(int id);
        ListDto<Achievement> GetAhievements(AchievementStatus status, AchievementState state, string type, int skip, int take);
    }
}