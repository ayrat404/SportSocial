using System;
using System.Drawing;
using System.IO;
using System.Web;
using DAL.DomainModel;
using DAL.Repository.Interfaces;

namespace BLL.Storage.Impls
{
    class FileService : IFileService
    {
        private readonly IRepository _repository;

        private const string VirtualImagePath = "/Storage/Images/"; //TODO â AppSettings

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
            if (!IsImage(inputStream))
            {
                result.Success = false;
                result.ErrorMessage = "File is not image";
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
            result.url = VirtualPathUtility.ToAbsolute(virtualPath);
            result.id = blogImage.Id;
            return result;
        }

        private string SaveFile(Stream fileStream, string fileName)
        {
            using (var stream = new MemoryStream())
            {
                string serverPath = HttpContext.Current.Server.MapPath(VirtualImagePath);

                fileStream.CopyTo(stream);
                var imagePath = Path.Combine(serverPath, fileName);
                File.WriteAllBytes(Path.Combine(imagePath, fileName), stream.ToArray());

                return Path.Combine(VirtualImagePath, fileName);
            }
        }
    }
}