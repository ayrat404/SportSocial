using BLL.Rating.Objects;
using BLL.Storage.Objects;

namespace BLL.Social.Journals.Objects
{
    public class MediaVm
    {
        public int Id { get; set; }
        public MediaType Type { get; set; }
        public string Url { get; set; }
        public string EmbeddedUrl { get; set; }
        public string RemoteUrl { get; set; }
        public RatingInfo Likes { get; set; }
    }
}