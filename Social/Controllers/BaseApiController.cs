using System.Web.Http;
using System.Web.Http.ModelBinding;
using BLL.Common.Objects;

namespace Social.Controllers
{
    public class BaseApiController : ApiController
    {
        protected ApiResult ModelStateErrors()
        {
            return new ApiResult();
        }

        protected ApiResult ApiResult(ServiceResult serviceResult)
        {
            return new ApiResult();
        }
    }
}
