using System;
using System.IO;
using System.Net.Http;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Storage.Objects;
using BLL.Storage.Objects.Enums;
using DAL.DomainModel.Base;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Storage.Impls
{
    public class VideoMediaWorker : MediaWorkerBase, IVideoMediaWorker
    {
        private readonly IRepository _repository;

        public VideoMediaWorker(IRepository repository)
        {
            _repository = repository;
        }

        public ServiceResult<VideoUploadResult> AddVideo(string url, UploadType uploadType)
        {
            return (ServiceResult<VideoUploadResult>)GetGenericMethod("AddVideo", uploadType)
                .Invoke(this, new object[]{url, GetPath(uploadType)});
        }

        private ServiceResult<VideoUploadResult> AddVideo<TEntity, TMedia>(string url, string savePath)
            where TEntity : class, IEntity
            where TMedia : MediaBase<TEntity>
        {
            if (!YoutubeUrlHelper.UrlIsValid(url))
            {
                return new ServiceResult<VideoUploadResult>
                {
                    Success = false,
                    ErrorMessage = "Невалидная ссылка на видео".Resource(this)
                };
            }
            var imageStream = DownloadImage(url);
            if (imageStream == null)
            {
                return ServiceResult.ErrorResult<VideoUploadResult>("Не удалось загрузить картинку-превью".Resource(this));
            }
            string serverFileName = Guid.NewGuid().ToString() + ".jpg";
            string fullName = Path.Combine(savePath, serverFileName);
            SaveFile(imageStream, fullName);
            var media = Activator.CreateInstance<TMedia>();
            media.Type = (int) MediaType.Video;
            media.Url = url;
            media.VideoImageUrl = fullName;
            media.VideoProvider = (int) VideoProvider.Youtube;
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
                    RemoteUrl = media.Url,
                    Id = media.Id
                }
            };
        }
        
        private Stream DownloadImage(string url)
        {
            string imageUrl = YoutubeUrlHelper.MaxResolutionImageUrl(url);
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(imageUrl).Result;
                if (!response.IsSuccessStatusCode)
                {
                    response = client.GetAsync(YoutubeUrlHelper.SdImageUrl(url)).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        response = client.GetAsync(YoutubeUrlHelper.LowImageUrl(url)).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            return null;
                            //result.Success = false;
                            //result.ErrorMessage = "Не удалось загрузить картинку-превью".Resource(this);
                            //_logger.Error("Не удалось загрузить картинку-превью. url={0}", youtubeUrl);
                            //return result;
                        }
                    }
                }
                return response.Content.ReadAsStreamAsync().Result;
            }
        }
    }
}