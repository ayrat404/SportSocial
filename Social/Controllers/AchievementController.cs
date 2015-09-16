using System.Web.Http;
using BLL.Social.Achievement;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/achievement")]
    [Authorize]
    public class AchievementController : BaseApiController
    {
        private readonly IAchievementService _achievementService;

        public AchievementController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        public ApiResult Temp()
        {
            
        }
    }
}
