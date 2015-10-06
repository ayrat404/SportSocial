using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using BLL.Common.Objects;
using BLL.Social.Journals.Objects;
using BLL.Storage.Objects;
using BLL.Storage.Objects.Enums;
using DAL.DomainModel.JournalEntities;

namespace BLL.Storage
{
    public interface IVideoMediaWorker
    {
        ServiceResult<VideoUploadResult> AddVideo(string url, UploadType uploadType);
    }
}