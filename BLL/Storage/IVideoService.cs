using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Social.Journals.Objects;
using BLL.Storage.Impls;
using BLL.Storage.Impls.Enums;
using BLL.Storage.Objects;
using DAL.DomainModel.Base;
using DAL.DomainModel.Interfaces;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Storage
{
    public interface IVideoService
    {
        ServiceResult<VideoUploadResult> AddVideo(string url, UploadType uploadType);

        void AttachVideosToEntity(List<MediaVm> vids, int entityId, UploadType uploadType);
    }

    public class VideoService : UploadServiceBase, IVideoService
    {
        private readonly IRepository _repository;

        public VideoService(IRepository repository)
        {
            _repository = repository;
        }

        public ServiceResult<VideoUploadResult> AddVideo(string url, UploadType uploadType)
        {
            return (ServiceResult<VideoUploadResult>)GetGenericMethod("AddVideo", uploadType)
                .Invoke(this, new object[]{url});
        }

        public void AttachVideosToEntity(List<MediaVm> vids, int entityId, UploadType uploadType)
        {
            GetGenericMethod("AttachVideosToEntity", uploadType).Invoke(this, new object[] { vids, entityId });
        }



        private ServiceResult<VideoUploadResult> AddVideo<TEntity, TMedia>(string url)
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
            var media = Activator.CreateInstance<TMedia>();
            media.Type = (int) MediaType.Video;
            media.Url = url;
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

        //public void AttachVideosToJournal(List<MediaVm> vids, int entityId)
        //{
        //    var mediaIds = vids.Select(v => v.Id);
        //    var medias = _repository.Queryable<JournalMedia>()
        //        .Where(m => mediaIds.Contains(m.Id) && m.EntityId == null)
        //        .ToList();
        //    foreach (var media in medias)
        //    {
        //        media.EntityId = entityId;
        //    }
        //    _repository.SaveChanges();
        //}

        private void AttachVideosToEntity<TEntity, TMedia>(List<MediaVm> vids, int entityId) 
            where TEntity : class, IEntity
            where TMedia : MediaBase<TEntity>
        {
            var mediaIds = vids.Select(v => v.Id);
            var medias = _repository.Queryable<TMedia>()
                .Where(m => mediaIds.Contains(m.Id) && m.EntityId == null)
                .ToList();
            foreach (var media in medias)
            {
                media.EntityId = entityId;
            }
            _repository.SaveChanges();
        }        
        
        
        private MethodInfo GetGenericMethod(string methodName, UploadType uploadType)
        {
            var methos = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
                return methos.MakeGenericMethod(GetEntity(uploadType), GetMediaEntity(uploadType));
        }


    }
}