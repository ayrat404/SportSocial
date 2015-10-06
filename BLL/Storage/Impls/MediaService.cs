using System.Collections.Generic;
using System.IO;
using BLL.Common.Objects;
using BLL.Social.Journals.Objects;
using BLL.Storage.Objects;
using BLL.Storage.Objects.Enums;

namespace BLL.Storage.Impls
{
    public class MediaService: IMediaService
    {
        private readonly IVideoMediaWorker _videoMediaWorker;
        private readonly IImageMediaWorker _imageMediaWorker;
        private readonly IAttacherMediaWorker _attacherMediaWorker;

        public MediaService(IVideoMediaWorker videoMediaWorker, IImageMediaWorker imageMediaWorker, IAttacherMediaWorker attacherMediaWorker)
        {
            _videoMediaWorker = videoMediaWorker;
            _imageMediaWorker = imageMediaWorker;
            _attacherMediaWorker = attacherMediaWorker;
        }

        public ServiceResult<ImageUploadResult> AddImage(Stream inputStream, string fileName, UploadType uploadType)
        {
            return _imageMediaWorker.AddImage(inputStream, fileName, uploadType);
        }

        public ServiceResult<VideoUploadResult> AddVideo(string url, UploadType uploadType)
        {
            return _videoMediaWorker.AddVideo(url, uploadType);
        }

        public void AttachMediaToEntity(List<MediaVm> medias, int entityId, UploadType type)
        {
            _attacherMediaWorker.AttachMediaToEntity(medias, entityId, type);
        }
    }
}