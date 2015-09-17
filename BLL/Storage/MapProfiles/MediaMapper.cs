using BLL.Common.Helpers;
using BLL.Rating;
using BLL.Social.Journals.Objects;
using BLL.Storage.Objects;
using DAL.DomainModel.Base;
using DAL.DomainModel.Interfaces;

namespace BLL.Storage.MapProfiles
{
    public class MediaMapper: MapperBase
    {
        public static MediaVm Map<T>(MediaBase<T> media) where T : IEntity
        {
            if (media == null)
                return null;
            return new MediaVm
            {
                Id = media.Id,
                Url = media.Url,
                EmbeddedUrl = YoutubeUrlHelper.EmbeddedYoutubeUrl(media.Url),
                RemoteUrl = media.Url,
                Type = (MediaType) media.Type
            };
        }
    }
}