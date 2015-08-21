using System;
using System.Drawing;
using System.IO;
using System.Web;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel.Base;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Storage.Impls
{
    public class ImageSaver<TEntity, TImageEntity> : IImageSaver
        where TEntity : IEntity 
        where TImageEntity : ImageBase<TEntity>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository _repository;

        public ImageSaver(ICurrentUser currentUser, IRepository repository)
        {
            _currentUser = currentUser;
            _repository = repository;
        }

        public ImageUploadResult Save(Stream inputStream, string savePath, string fileName)
        {
            var result = new ImageUploadResult()
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
            _repository.Add(imageEntity);
            _repository.SaveChanges();
            result.Url = VirtualPathUtility.ToAbsolute(url);
            result.Id = imageEntity.Id;
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