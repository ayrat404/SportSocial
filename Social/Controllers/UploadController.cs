using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using BLL.Storage;
using BLL.Storage.Impls;
using BLL.Storage.Objects.Enums;
using Social.Models;

namespace Social.Controllers
{

    [RoutePrefix("api/upload")]
    public class UploadController : BaseApiController
    {
        private readonly IMediaService _mediaService;
        //private readonly IVideoService _videoService;

        public UploadController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [Route("")]
        [HttpPost]
        public ApiResult Upload()
        {
            string key = HttpContext.Current.Request.Files.AllKeys.First();
            var uploadType = (UploadType)Enum.Parse(typeof (UploadType), key);
            var file = HttpContext.Current.Request.Files[key];
            var result = _mediaService.AddImage(file.InputStream, file.FileName, uploadType);
            return ApiResult(result);
        }

        [Route("~/api/youtube")]
        [HttpPost]
        public ApiResult AddYoutubeVideo(VideoLink link)
        {
            var result = _mediaService.AddVideo(link.Link, link.Type);
            var result2 =  ApiResult(result);
            return result2;
        }
    }
}
