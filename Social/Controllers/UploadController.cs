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
using Social.Models;

namespace Social.Controllers
{

    [RoutePrefix("api/upload")]
    public class UploadController : BaseApiController
    {
        private readonly IImageService _imageService;
        private readonly IVideoService _videoService;

        public UploadController(IImageService imageService, IVideoService videoService)
        {
            _imageService = imageService;
            _videoService = videoService;
        }

        [Route("")]
        [HttpPost]
        public ApiResultBase Upload()
        {
            string key = HttpContext.Current.Request.Files.AllKeys.First();
            var uploadType = (UploadType)Enum.Parse(typeof (UploadType), key);
            var file = HttpContext.Current.Request.Files[key];
            var result = _imageService.Save(file.InputStream, file.FileName, uploadType);
            return ApiResult(result);
        }

        [Route("~/api/youtube")]
        [HttpPost]
        public ApiResultBase AddYoutubeVideo(VideoLink link)
        {
            var result = _videoService.AddVideo(link.Link);
            var result2 =  ApiResult(result);
            return result2;
        }
    }

    public class VideoLink
    {
        public string Link { get; set; }
    }
}
