using System.IO;
using BLL.Common.Objects;
using BLL.Storage.Impls.Enums;

namespace BLL.Storage
{
    public interface IFileService
    {
        bool IsImage(Stream inputStream);
        ImageUploadResult UploadImage(Stream inputStream, string fileName, UploadType uploadType);
        ServiceResult UploadAvatar(Stream inputStream, string filePath);
        ImageUploadResult YoutubeImage(string youtubeUrl);
    }

    public class ImageUploadResult: ServiceResult
    {
        public string Url { get; set; }

        public int Id { get; set; }
   }
}