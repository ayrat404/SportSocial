using System.Collections.Generic;
using DAL.DomainModel.Achievement;

namespace DAL.Repository.Interfaces
{
    public interface IAchievementRepository: IRepository
    {
        List<Achievement> GetThreeRandomAchievements();
        Achievement GetTempAchievement(int userId);
    }
}