using System.IO;
using BLL.Common.Objects;
using BLL.Storage.Objects.Enums;

namespace BLL.Storage
{
    public interface IImageMediaWorker
    {
        ServiceResult<ImageUploadResult> AddImage(Stream inputStream, string fileName, UploadType uploadType);
    }
}