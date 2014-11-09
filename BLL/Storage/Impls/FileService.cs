using System;
using System.Drawing;
using System.IO;
using System.Web;
using DAL.DomainModel;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Storage.Impls
{
    public class FileService : IFileService
    {
        private readonly IRepository _repository;

        private const string VirtualImagePath = "/Storage/Images/"; //TODO в AppSettings

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

        public ImageUploadResult UploadBlogImage(Stream inputStream, string fileName)
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
                var blogImage = new BlogImage()
                {
                    ContentType = fileExtensioin,
                };
                _repository.Add(blogImage);
                _repository.SaveChanges();
                var virtualPath = SaveFile(inputStream, blogImage.Id + fileExtensioin);
                result.Url = VirtualPathUtility.ToAbsolute(virtualPath);
                result.Id = blogImage.Id;
                return result;
            }
        }

        private string SaveFile(Stream fileStream, string fileName)
        {
            using (var stream = new MemoryStream())
            {
                string serverPath = HttpContext.Current.Server.MapPath(VirtualImagePath);

                fileStream.Position = 0;
                fileStream.CopyTo(stream);
                var imagePath = Path.Combine(serverPath, fileName);
                File.WriteAllBytes(imagePath, stream.ToArray());

                return Path.Combine(VirtualImagePath, fileName);
            }
        }
    }
}