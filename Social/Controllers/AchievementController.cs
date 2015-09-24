using System.Web.Http;
using BLL.Common.Objects;
using BLL.Social.Achievements;
using BLL.Social.Achievements;
using BLL.Social.Achievements.Objects;
using DAL.DomainModel.Achievement.Objects;
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

        [Route("temp")]
        [HttpDelete]
        public ApiResult DeleteTemp()
        {
            return ApiResult(_achievementService.DeleteTemp());
        }

        [Route("filter")]
        [HttpGet]
        public ApiResult Filter()
        {
            return ApiResult(new {types = _achievementService.GetFilter()});
        }

        [Route("")]
        public ApiResult Get(int id)
        {
            return ApiResult(_achievementService.GetAchivement(id));
        }

        [Route("~/api/achievements")]
        public ApiResult Get(AchievementState actual = AchievementState.All, AchievementStatus status = AchievementStatus.All, string type = null, int page = 1, int count = 20)
        {
            var search = new AchievementSearch
            {
                Count = count,
                Status = status,
                Type = type,
                Page = page,
                Actual = actual
            };
            return ApiResult(_achievementService.GetStartedAchivements(search));
        }

        [Route("voice")]
        [HttpPost]
        public ApiResult Vote(AchievementVoteVm vote)
        {
            return ApiResult(_achievementService.Vote(vote));
        }

    }
}
