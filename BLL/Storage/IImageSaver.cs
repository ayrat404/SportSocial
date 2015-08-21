using System.IO;

namespace BLL.Storage
{
    public interface IImageSaver
    {
        ImageUploadResult Save(Stream inputStream, string savePath, string fileName);
    }
}