using System;
using System.Collections.Generic;
using System.IO;
using BLL.Comments.Impls;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Social.Journals.Objects;
using BLL.Storage.Impls.Enums;
using DAL.DomainModel;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;

namespace BLL.Storage.Impls
{
    public abstract class UploadServiceBase
    {
        protected Type GetMediaEntity(UploadType type)
        {
            switch (type)
            {
                case UploadType.Avatar:
                    return typeof(UserAvatarPhoto);
                case UploadType.Article:
                    return typeof(BlogImage);
                case UploadType.Journal:
                    return typeof(JournalMedia);
                case UploadType.Achievement:
                    return typeof(AchievementMedia);
            }
            throw new NotSupportedException();
        }

        protected Type GetEntity(UploadType type)
        {
            switch (type)
            {
                case UploadType.Avatar:
                    return typeof(AppUser);
                case UploadType.Article:
                    return typeof(Post);
                case UploadType.Journal:
                    return typeof(Journal);
                case UploadType.Achievement:
                    return typeof(Achievement);
            }
            throw new NotSupportedException();
        }
    }

    public class UniversalImageService: UploadServiceBase, IImageService
    {
        private const string VirtualBlogImagePath = "/Storage/Images/Blog/"; //TODO â AppSettings
        private const string VirtualJournslImagePath = "/Storage/Images/Journal/"; //TODO â AppSettings
        private const string VirtualAvatarImagePath = "/Storage/User/Avatars/"; //TODO â AppSettings

        private readonly ICurrentUser _currentUser;
        private readonly IRepository _repository;

        public UniversalImageService(ICurrentUser currentUser, IRepository repository)
        {
            _currentUser = currentUser;
            _repository = repository;
        }

        public ServiceResult<ImageUploadResult> Save(Stream inputStream, string fileName, UploadType uploadType)
        {
            return CreateGenericService(uploadType).Save(inputStream, GetPath(uploadType), fileName);
        }

        public void AttachImagesToEntity(List<MediaVm> medias, int entityId, UploadType type)
        {
            CreateGenericService(type).AttachImagesToEntity(medias, entityId);
        }

        private IImageSaver CreateGenericService(UploadType type)
        {
            Type ratingServiseType = typeof(ImageSaver<,>)
                .MakeGenericType(GetEntity(type), GetMediaEntity(type));
            return (IImageSaver)Activator.CreateInstance(ratingServiseType, _currentUser, _repository);
        }

        private string GetPath(UploadType type)
        {
            switch (type)
            {
                case UploadType.Avatar:
                    return VirtualAvatarImagePath;
                case UploadType.Article:
                    return VirtualBlogImagePath;
                case UploadType.Journal:
                    return VirtualJournslImagePath;
            }
            throw new NotSupportedException();
        }
    }
}