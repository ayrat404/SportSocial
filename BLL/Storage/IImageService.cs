using System.IO;
using BLL.Storage.Impls.Enums;

namespace BLL.Storage
{
    public interface IImageService
    {
        ImageUploadResult Save(Stream inputStream, string fileName, UploadType uploadType);
    }
}