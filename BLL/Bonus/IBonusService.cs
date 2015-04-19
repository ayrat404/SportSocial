using System.Collections.Generic;
using BLL.Bonus.Objects;

namespace BLL.Bonus
{
    public interface IBonusService
    {
        bool HaveAccess();
        List<BonusVideoModel> GetBonusVideos();
        bool CanViewVideo(int videoId);
        string GetVideoFilePAth(string fileName);
    }
}