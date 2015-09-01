using System.ComponentModel.DataAnnotations;
using System.Web;
using BLL.Storage.Impls.Enums;

namespace BLL.Storage.Objects
{
    public class UploadImageModel
    {
        [Required]
        public HttpPostedFileBase Image { get; set; }

        [Required]
        public UploadType Type { get; set; }
    }
}