using System;
using System.IO;
using BLL.Comments.Impls;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Storage.Impls.Enums;
using DAL.DomainModel;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;

namespace BLL.Storage.Impls
{
    public class UniversalImageService: IImageService
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

        private IImageSaver CreateGenericService(UploadType type)
        {
            Type ratingServiseType = typeof(ImageSaver<,>)
                .MakeGenericType(GetImageEntity(type), GetEntity(type));
            return (IImageSaver)Activator.CreateInstance(ratingServiseType, _currentUser, _repository);
        }

        private Type GetEntity(UploadType type)
        {
            switch (type)
            {
                case UploadType.Avatar:
                    return typeof (UserAvatarPhoto);
                case UploadType.Article:
                    return typeof (BlogImage);
                case UploadType.Journal:
                    return typeof (JournalMedia);
            }
            throw new NotSupportedException();
        }

        private Type GetImageEntity(UploadType type)
        {
            switch (type)
            {
                case UploadType.Avatar:
                    return typeof (AppUser);
                case UploadType.Article:
                    return typeof (Post);
                case UploadType.Journal:
                    return typeof (Journal);
            }
            throw new NotSupportedException();
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