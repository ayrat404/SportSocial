using System.IO;
using BLL.Common.Objects;
using BLL.Storage.Objects.Enums;

namespace BLL.Storage
{
    public interface IFileService
    {
        bool IsImage(Stream inputStream);
        LegacyImageUploadResult UploadImage(Stream inputStream, string fileName, UploadType uploadType);
        ServiceResult UploadAvatar(Stream inputStream, string filePath);
        LegacyImageUploadResult YoutubeImage(string youtubeUrl);
        void SaveFile(Stream fileStream, string filePath);
    }

    public class LegacyImageUploadResult : ServiceResult
    {
        public string Url { get; set; }

        public int Id { get; set; }
    }

    public class ImageUploadResult
    {
        public string Url { get; set; }

        public int Id { get; set; }
   }
}