using System.Collections.Generic;
using System.IO;
using BLL.Common.Objects;
using BLL.Social.Journals.Objects;
using BLL.Storage.Objects;
using BLL.Storage.Objects.Enums;

namespace BLL.Storage
{
    public interface IMediaService
    {
        ServiceResult<ImageUploadResult> AddImage(Stream inputStream, string fileName, UploadType uploadType);
        ServiceResult<VideoUploadResult> AddVideo(string url, UploadType uploadType);
        void AttachMediaToEntity(List<MediaVm> medias, int entityId, UploadType type);
    }
}