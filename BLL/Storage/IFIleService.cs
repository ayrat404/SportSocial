using System.IO;
using BLL.Common.Objects;

namespace BLL.Storage
{
    public interface IFileService
    {
        bool IsImage(Stream inputStream);
        ImageUploadResult UploadBlogImage(Stream inputStream, string fileName);
    }

    public class ImageUploadResult: ServiceResult
    {
        public string Url { get; set; }

        public int Id { get; set; }
   }
}