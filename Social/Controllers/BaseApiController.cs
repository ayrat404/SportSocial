using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using BLL.Common.Objects;
using BundleTransformer.Core.Filters;
using Social.Models;

namespace Social.Controllers
{
    public class BaseApiController : ApiController
    {
        protected ApiResult ModelStateErrors()
        {
            var result = new ApiResult {Success = false};
            if (ModelState.IsValid)
            {
                result.Success = true;
                return result;
            }
            result.Errors = new ApiError
            {
                Fields = new List<FieldError>()
            };
            var fields = ModelState.Keys.ToList();
            var values = ModelState.Values.ToList();
            for (int i = 0; i < fields.Count; i++)
            {
                var error = values[i].Errors.First().ErrorMessage;
                var fieldError = new FieldError
                {
                    Name = fields[i],
                    Error = error
                };
                result.Errors.Fields.Add(fieldError);
            }
            return new ApiResult();
        }

        protected ApiResult ApiResult(ServiceResult serviceResult)
        {
            var result = new ApiResult {Success = serviceResult.Success};
            if (result.Success) return result;
            result.Errors = new ApiError
            {
                Common = new SummaryError
                {
                    Error = serviceResult.ErrorMessage
                }
            };
            return result;
        }        
        
        protected ApiResult<T> ApiResult<T>(ServiceResult<T> serviceResult) where T: class 
        {
            var result = ApiResult((ServiceResult)serviceResult);
            var resG = new ApiResult<T>
            {
                Errors = result.Errors,
                Message = result.Message,
                Success = result.Success,
                Data = serviceResult.Result
            };
            return resG;
        }
    }
}
