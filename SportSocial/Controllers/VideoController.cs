using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using BLL.Bonus;
using BLL.Bonus.Impls;
using BLL.Common.Helpers;
using BLL.Common.Services.CurrentUser.Impls;
using BLL.Infrastructure.IdentityConfig;
using DAL;
using DAL.Repository;

namespace SportSocial.Controllers
{
    [Authorize]
    public class VideoController : ApiController
    {
        private readonly IBonusService _bonusService;

        public VideoController()
        {
            var context = new EntityDbContext();
            _bonusService = new BonusService(new Repository(context), new CurrentUser(new AppUserManager(new AppUserStore(context))));
        }

        [Route("api/play")]
        [HttpGet]
        public HttpResponseMessage Play(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || FileStreamingHelper.AnyInvalidFileNameChars(id))
                throw new HttpResponseException(HttpStatusCode.NotFound);

            int videoId;
            if (!int.TryParse(Path.GetFileNameWithoutExtension(id), out videoId))
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //if (!_bonusService.CanViewVideo(videoId))
            //    throw new HttpResponseException(HttpStatusCode.NotFound);

            string filePath = _bonusService.GetVideoFilePAth(id);

            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            long totalLength = fileInfo.Length;

            RangeHeaderValue rangeHeader = base.Request.Headers.Range;
            HttpResponseMessage response = new HttpResponseMessage();

            response.Headers.AcceptRanges.Add("bytes");

            if (rangeHeader == null || !rangeHeader.Ranges.Any())
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new PushStreamContent((outputStream, httpContent, transpContext)
                =>
                {
                    using (outputStream) // Copy the file to output stream straightforward. 
                    using (Stream inputStream = fileInfo.OpenRead())
                    {
                        try
                        {
                            inputStream.CopyTo(outputStream, FileStreamingHelper.ReadStreamBufferSize);
                        }
                        catch (Exception error)
                        {
                            Debug.WriteLine(error);
                        }
                    }
                }, FileStreamingHelper.GetMimeNameFromExt(fileInfo.Extension));

                response.Content.Headers.ContentLength = totalLength;
                return response;
            }

            long start = 0, end = 0;

            // 1. If the unit is not 'bytes'.
            // 2. If there are multiple ranges in header value.
            // 3. If start or end position is greater than file length.
            if (rangeHeader.Unit != "bytes" || rangeHeader.Ranges.Count > 1 ||
                !FileStreamingHelper.TryReadRangeItem(rangeHeader.Ranges.First(), totalLength, out start, out end))
            {
                response.StatusCode = HttpStatusCode.RequestedRangeNotSatisfiable;
                response.Content = new StreamContent(Stream.Null);  // No content for this status.
                response.Content.Headers.ContentRange = new ContentRangeHeaderValue(totalLength);
                response.Content.Headers.ContentType = FileStreamingHelper.GetMimeNameFromExt(fileInfo.Extension);

                return response;
            }

            var contentRange = new ContentRangeHeaderValue(start, end, totalLength);

            // We are now ready to produce partial content.
            response.StatusCode = HttpStatusCode.PartialContent;
            response.Content = new PushStreamContent((outputStream, httpContent, transpContext)
            =>
            {
                using (outputStream) // Copy the file to output stream in indicated range.
                using (Stream inputStream = fileInfo.OpenRead())
                    FileStreamingHelper.CreatePartialContent(inputStream, outputStream, start, end);

            }, FileStreamingHelper.GetMimeNameFromExt(fileInfo.Extension));

            response.Content.Headers.ContentLength = end - start + 1;
            response.Content.Headers.ContentRange = contentRange;

            return response;

        }
    }
}