using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Social.Journals.Objects;
using DAL.DomainModel;
using DAL.DomainModel.Base;
using DAL.DomainModel.Interfaces;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Storage.Impls
{
    public class ImageSaver<TEntity, TImageEntity> : IImageSaver
        where TEntity : IEntity 
        where TImageEntity : MediaBase<TEntity>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository _repository;

        public ImageSaver(ICurrentUser currentUser, IRepository repository)
        {
            _currentUser = currentUser;
            _repository = repository;
        }

        public ServiceResult<ImageUploadResult> Save(Stream inputStream, string savePath, string fileName)
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
            var imageEntity = (TImageEntity)Activator.CreateInstance(typeof (TImageEntity));
            imageEntity.Url = url;
            imageEntity.UserId = _currentUser.UserId;
            _repository.Add(imageEntity);
            //TODO костыль
            if (typeof (TEntity) == typeof (AppUser))
            {
                _currentUser.User.Profile.Avatar = url;
                imageEntity.EntityId = _currentUser.UserId;
            }
            _repository.SaveChanges();
            result.Result = new ImageUploadResult();
            result.Result.Url = VirtualPathUtility.ToAbsolute(url);
            result.Result.Id = imageEntity.Id;
            return result;
        }

        public void AttachImagesToEntity(List<MediaVm> addedMedias, int entityId)
        {
            var mediaIds = addedMedias.Select(v => v.Id);
            var medias = _repository.Queryable<TImageEntity>()
                .Where(m => mediaIds.Contains(m.Id) && m.EntityId == null)
                .ToList();
            foreach (var media in medias)
            {
                media.EntityId = entityId;
            }
            _repository.SaveChanges();
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

        private void SaveFile(Stream fileStream, string filePath)
        {
            using (var stream = new MemoryStream())
            {
                string serverPath = HttpContext.Current.Server.MapPath(filePath);
                string dir = Path.GetDirectoryName(serverPath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                fileStream.Position = 0;
                fileStream.CopyTo(stream);
                File.WriteAllBytes(serverPath, stream.ToArray());
            }
        }
    }
}