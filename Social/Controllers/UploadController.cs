using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using BLL.Storage;
using BLL.Storage.Impls.Enums;
using BLL.Storage.ViewModels;

namespace Social.Controllers
{

    [RoutePrefix("api/upload")]
    public class UploadController : BaseApiController
    {
        private readonly IImageService _imageService;

        public UploadController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [Route("")]
        public ApiResultBase Upload()
        {
            string key = HttpContext.Current.Request.Files.AllKeys.First();
            var uploadType = (UploadType)Enum.Parse(typeof (UploadType), key);
            var file = HttpContext.Current.Request.Files[key];
            var result = _imageService.Save(file.InputStream, file.FileName, uploadType);
            return new ApiResult<ImageUploadResult>() {Result = result};
        }
    }
}
