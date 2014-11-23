using System.Web;
using BLL.Storage.Impls.Enums;

namespace BLL.Storage.ViewModels
{
    public class UploadImageModel
    {
        public HttpPostedFileBase Image { get; set; }

        public UploadType Type { get; set; }
    }
}