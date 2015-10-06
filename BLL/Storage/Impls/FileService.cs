using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Storage.Objects.Enums;
using DAL.DomainModel;
using DAL.Repository.Interfaces;
using Knoema.Localization;
using NLog;

namespace BLL.Storage.Impls
{
    public class FileService : IFileService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

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

        public ServiceResult<ImageUploadResult> UploadImage(Stream inputStream, string fileName, UploadType uploadType)
        {
            var result = new ServiceResult<ImageUploadResult>
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
                    case UploadType.Article:
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
                        string virtualPath = Path.Combine(VirtualPhotosImagePath, _currentUser.UserId + "/");
                        url = string.Concat(virtualPath, serverFileName);
                        var avatarImage = new UserAvatarPhoto()
                        {
                            EntityId = _currentUser.UserId,
                            Url = url,
                        };
                        _repository.Add(avatarImage);
                        _currentUser.User.Profile.Avatar = url;
                        _repository.SaveChanges();
                        id = avatarImage.Id;
                        break;
                    default:
                        result.Success = false;
                        return result;
                }
                SaveFile(inputStream, url);
                result.Result = new ImageUploadResult();
                result.Result.Url = VirtualPathUtility.ToAbsolute(url);
                result.Result.Id = id;
                return result;
            }
        }

        public ServiceResult UploadAvatar(Stream inputStream, string fileName)
        {
            throw new NotImplementedException();
        }

        public void SaveFile(Stream fileStream, string filePath)
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

        public ServiceResult<ImageUploadResult> YoutubeImage(string youtubeUrl)
        {
            var result = new ServiceResult<ImageUploadResult>{Success = true};
            if (!YoutubeUrlHelper.UrlIsValid(youtubeUrl))
            {
                result.Success = false;
                result.ErrorMessage = "Некорректный формат ссылки на видео".Resource(this);
                return result;
            }
            string imageUrl = YoutubeUrlHelper.MaxResolutionImageUrl(youtubeUrl);
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(imageUrl).Result;
                if (!response.IsSuccessStatusCode)
                {
                    response = client.GetAsync(YoutubeUrlHelper.SdImageUrl(youtubeUrl)).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        response = client.GetAsync(YoutubeUrlHelper.LowImageUrl(youtubeUrl)).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            result.Success = false;
                            result.ErrorMessage = "Не удалось загрузить картинку-превью".Resource(this);
                            _logger.Error("Не удалось загрузить картинку-превью. url={0}", youtubeUrl);
                            return result;
                        }
                    }
                }
                var imageBytes = response.Content.ReadAsByteArrayAsync().Result;
                using (var stream = new MemoryStream(imageBytes))
                {
                    return UploadImage(stream, YoutubeUrlHelper.MaxResImageName, UploadType.Article);
                }
            }
        }
    }
}