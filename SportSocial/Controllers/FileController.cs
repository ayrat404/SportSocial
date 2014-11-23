using System;
using System.Web;
using System.Web.Mvc;
using BLL.Common.Objects;
using BLL.Storage;
using BLL.Storage.Impls.Enums;
using BLL.Storage.ViewModels;
using Knoema.Localization;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class FileController : SportSocialControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public ActionResult Images(HttpPostedFileBase uploaderHelper)
        {
            //return Json(new {success = true, id = 3288388, url = "/storage/images/1.jpg"});
            var result = _fileService.UploadImage(uploaderHelper.InputStream, uploaderHelper.FileName, UploadType.Post);
            return Json(result);
        }

        public ActionResult UploadImage(UploadImageModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Type)
                {
                    case UploadType.Avatar:
                        return Json(_fileService.UploadAvatar(model.Image.InputStream, model.Image.FileName));
                    case UploadType.Post:
                        return Json(_fileService.UploadImage(model.Image.InputStream, model.Image.FileName, UploadType.Post));
                    case UploadType.Album:
                        throw new NotImplementedException();
                }
            }
            return Json(new ServiceResult {Success = false, ErrorMessage = "Ошибка".Resource(this)});
        }
	}
}