using BLL.Storage.Objects.Enums;

namespace Social.Models
{
    public class VideoLink
    {
        public UploadType Type { get; set; }

        public string Link { get; set; }
    }
}