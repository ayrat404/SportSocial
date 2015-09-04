using System.Collections.Generic;
using System.IO;
using BLL.Common.Objects;
using BLL.Social.Journals.Objects;

namespace BLL.Storage
{
    public interface IImageSaver
    {
        ServiceResult<ImageUploadResult> Save(Stream inputStream, string savePath, string fileName);
        
        void AttachImagesToEntity(List<MediaVm> addedMedias, int entityId);
    }
}