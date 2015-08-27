using System.IO;
using BLL.Common.Objects;

namespace BLL.Storage
{
    public interface IImageSaver
    {
        ServiceResult<ImageUploadResult> Save(Stream inputStream, string savePath, string fileName);
    }
}