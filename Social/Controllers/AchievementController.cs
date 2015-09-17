using System.Web.Http;
using BLL.Common.Objects;
using BLL.Social.Achievements;
using BLL.Social.Achievements;
using BLL.Social.Achievements.Objects;
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

        [Route("temp")]
        [HttpGet]
        public ApiResult Temp()
        {
            return ApiResult(_achievementService.FirstStep());
        }


        [Route("temp")]
        [HttpPost]
        public ApiResult Temp(AchievementCreateVm model)
        {
            return ApiResult(_achievementService.CreateOrUpdateAchievement(model));

        }        
        
        [Route("temp")]
        [HttpPut]
        public ApiResult TempUpdate(AchievementCreateVm model)
        {
            return ApiResult(_achievementService.CreateOrUpdateAchievement(model));
        }

        [Route("filter")]
        [HttpGet]
        public ApiResult Filter()
        {
            return ApiResult(new {types = _achievementService.GetFilter()});
        }

        [Route("{id}")]
        public ApiResult Get(int id)
        {
            return ApiResult(_achievementService.GetAchivement(id));
        }

    }
}
