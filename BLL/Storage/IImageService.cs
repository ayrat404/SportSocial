using System.Collections.Generic;
using System.IO;
using BLL.Common.Objects;
using BLL.Social.Journals.Objects;
using BLL.Storage.Impls.Enums;

namespace BLL.Storage
{
    public interface IImageService
    {
        ServiceResult<ImageUploadResult> Save(Stream inputStream, string fileName, UploadType uploadType);

        void AttachImagesToEntity(List<MediaVm> medias, int entityId, UploadType type);
    }
}