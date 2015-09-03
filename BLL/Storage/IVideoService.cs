using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Storage.Objects;
using DAL.DomainModel.Base;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Storage
{
    public interface IVideoService
    {
        ServiceResult<VideoUploadResult> AddVideo(string url);
    }

    public class VideoService : IVideoService
    {
        private readonly IRepository _repository;

        public VideoService(IRepository repository)
        {
            _repository = repository;
        }

        public ServiceResult<VideoUploadResult> AddVideo(string url)
        {
            if (!YoutubeUrlHelper.UrlIsValid(url))
            {
                return new ServiceResult<VideoUploadResult>
                {
                    Success = false,
                    ErrorMessage = "Невалидная ссылка на видео".Resource(this)
                };
            }
            var media = new JournalMedia
            {
                Type = (int) MediaType.Video,
                Url = url,
                VideoProvider = (int) VideoProvider.Youtube,
            };
            _repository.Add(media);
            _repository.SaveChanges();
            return new ServiceResult<VideoUploadResult>
            {
                Success = true,
                Result = new VideoUploadResult
                {
                    EmbeddedUrl = YoutubeUrlHelper.EmbeddedYoutubeUrl(url),
                    RemoteId = YoutubeUrlHelper.VideoId(url),
                    Img = YoutubeUrlHelper.MaxResolutionImageUrl(url),
                    Id = media.Id
                }
            };
        }
    }
}