using System;
using System.Drawing;
using System.IO;
using System.Web;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Storage.Objects.Enums;
using DAL.DomainModel;
using DAL.DomainModel.Base;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Storage.Impls
{
    public class ImageMediaWorker : MediaWorkerBase, IImageMediaWorker
    {

        private readonly ICurrentUser _currentUser;
        private readonly IRepository _repository;

        public ImageMediaWorker(ICurrentUser currentUser, IRepository repository)
        {
            _currentUser = currentUser;
            _repository = repository;
        }

        public ServiceResult<ImageUploadResult> AddImage(Stream inputStream, string fileName, UploadType uploadType)
        {
            if (uploadType == UploadType.Avatar && !_currentUser.IsAnonimous)
            {
                var result = AddImage<AppUser, UserAvatarPhoto>(inputStream, GetPath(uploadType), fileName);
                _currentUser.User.Profile.Avatar = result.Result.Url;
                return result;
            }
            return (ServiceResult<ImageUploadResult>)GetGenericMethod("AddImage", uploadType)
                .Invoke(this, new object [] { inputStream, GetPath(uploadType), fileName });
        }


        private ServiceResult<ImageUploadResult> AddImage<TEntity, TMedia>(Stream inputStream, string savePath, string fileName)
            where TEntity : class, IEntity
            where TMedia : MediaBase<TEntity>
        {
            var result = new ServiceResult<ImageUploadResult>
            {
                Success = true,
            };
            if (!IsImage(inputStream))
            {
                result.Success = false;
                result.ErrorMessage = "Загружаемый файл не является изображением".Resource(this);
                return result;
            }
            string serverFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
            string url = Path.Combine(savePath, serverFileName);
            SaveFile(inputStream, url);
            var imageEntity = (TMedia)Activator.CreateInstance(typeof(TMedia));
            imageEntity.Url = url;
            if (_currentUser.UserId > 0)
            {
                imageEntity.UserId = _currentUser.UserId;
            }
            _repository.Add(imageEntity);
            _repository.SaveChanges();
            result.Result = new ImageUploadResult
            {
                Url = VirtualPathUtility.ToAbsolute(url),
                Id = imageEntity.Id
            };
            return result;

        }

        private bool IsImage(Stream inputStream)
        {
            try
            {
                using (var bitmap = new Bitmap(inputStream))
                {
                    if (bitmap.Size.IsEmpty)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}