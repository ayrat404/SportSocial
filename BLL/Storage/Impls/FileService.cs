using System;
using System.Drawing;
using System.IO;
using System.Web;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Storage.Impls.Enums;
using DAL.DomainModel;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Storage.Impls
{
    public class FileService : IFileService
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        private const string VirtualBlogImagePath = "/Storage/Images/"; //TODO в AppSettings
        private const string VirtualPhotosImagePath = "/Storage/User/"; //TODO в AppSettings

        public FileService(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public bool IsImage(Stream inputStream)
        {
            try  
            {
                using (var bitmap = new Bitmap(inputStream))
                {
                    if(bitmap.Size.IsEmpty)
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

        public ImageUploadResult UploadImage(Stream inputStream, string fileName, UploadType uploadType)
        {
            var result = new ImageUploadResult()
            {
                Success = true,
            };
            if (inputStream == null)
            {
                result.Success = false;
                //result.ErrorMessage = "Empty stream";
                return result;
            }
            using (inputStream)
            {
                if (!IsImage(inputStream))
                {
                    result.Success = false;
                    result.ErrorMessage = "Загружаемый файл не является изображением".Resource(this);
                    return result;
                }
                int id = 0;
                string serverFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);;
                string url = string.Empty;
                switch (uploadType)
                {
                    case UploadType.Post:
                        url = Path.Combine(VirtualBlogImagePath, serverFileName);
                        var blogImage = new BlogImage()
                        {
                            Url = url,
                        };
                        _repository.Add(blogImage);
                        _repository.SaveChanges();
                        id = blogImage.Id;
                        break;
                    case UploadType.Avatar:
                        string virtualPath = Path.Combine(VirtualPhotosImagePath, _currentUser.UserId);
                        if (!Directory.Exists(virtualPath))
                            Directory.CreateDirectory(virtualPath);
                        url = Path.Combine(virtualPath, url);
                        var avatarImage = new UserAvatarPhoto()
                        {
                            UserId = _currentUser.UserId,
                            Url = url,
                        };
                        _repository.Add(avatarImage);
                        _repository.SaveChanges();
                        id = avatarImage.Id;
                        break;
                }
                SaveFile(inputStream, url);
                result.Url = VirtualPathUtility.ToAbsolute(url);
                result.Id = id;
                return result;
            }
        }

        public ServiceResult UploadAvatar(Stream inputStream, string fileName)
        {
            throw new NotImplementedException();
        }

        private void SaveFile(Stream fileStream, string filePath)
        {
            using (var stream = new MemoryStream())
            {
                string serverPath = HttpContext.Current.Server.MapPath(filePath);

                fileStream.Position = 0;
                fileStream.CopyTo(stream);
                File.WriteAllBytes(serverPath, stream.ToArray());
            }
        }
    }
}