using System;
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
                int dotIndex = fields[i].IndexOf(".");
                var fieldName = fields[i].Substring(dotIndex + 1, fields[i].Length - dotIndex - 1);
                string fieldNameCamelCase = fieldName.Substring(0, 1).ToLower() +
                                            fieldName.Substring(1, fieldName.Length - 1);
                var fieldError = new FieldError
                {
                    Name = fieldNameCamelCase,
                    Error = error
                };
                result.Errors.Fields.Add(fieldError);
            }
            return result;
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
