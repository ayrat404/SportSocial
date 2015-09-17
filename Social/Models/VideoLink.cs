using BLL.Storage.Impls.Enums;

namespace Social.Models
{
    public class VideoLink
    {
        public UploadType Type { get; set; }

        public string Link { get; set; }
    }
}