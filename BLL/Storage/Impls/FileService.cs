using System;
using System.Drawing;
using System.IO;
using System.Web;
using BLL.Common.Objects;
using BLL.Storage.Impls.Enums;
using DAL.DomainModel;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Storage.Impls
{
    public class FileService : IFileService
    {
        private readonly IRepository _repository;

        private const string VirtualBlogImagePath = "/Storage/Images/"; //TODO в AppSettings
        private const string VirtualPhotosImagePath = "/Storage/Images/"; //TODO в AppSettings

        public FileService(IRepository repository)
        {
            _repository = repository;
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
                string fileExtensioin = Path.GetExtension(fileName);
                int id = 0;
                switch (uploadType)
                {
                    case UploadType.Post:
                        var blogImage = new BlogImage()
                        {
                            ContentType = fileExtensioin,
                            Name = fileName,
                        };
                        _repository.Add(blogImage);
                        _repository.SaveChanges();
                        id = blogImage.Id;
                        break;
                    case UploadType.Avatar:
                        //var avatarImage = new UserAvatarPhoto()
                        //{
                            
                        //};
                        //_repository.Add(blogImage);
                        //_repository.SaveChanges();
                        //id = blogImage.Id;
                        break;
                }
                var virtualPath = SaveFile(inputStream, id + fileExtensioin, VirtualBlogImagePath);
                result.Url = VirtualPathUtility.ToAbsolute(virtualPath);
                result.Id = id;
                return result;
            }
        }

        public ServiceResult UploadAvatar(Stream inputStream, string fileName)
        {
            throw new NotImplementedException();
        }

        private string SaveFile(Stream fileStream, string fileName, string virtualPath)
        {
            using (var stream = new MemoryStream())
            {
                string serverPath = HttpContext.Current.Server.MapPath(virtualPath);

                fileStream.Position = 0;
                fileStream.CopyTo(stream);
                var imagePath = Path.Combine(serverPath, fileName);
                File.WriteAllBytes(imagePath, stream.ToArray());
                return Path.Combine(virtualPath, fileName);
            }
        }
    }
}