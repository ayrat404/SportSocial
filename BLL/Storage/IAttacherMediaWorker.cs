using System.Collections.Generic;
using BLL.Social.Journals.Objects;
using BLL.Storage.Objects.Enums;

namespace BLL.Storage
{
    public interface IAttacherMediaWorker
    {
        void AttachMediaToEntity(List<MediaVm> vids, int entityId, UploadType uploadType);
    }
}