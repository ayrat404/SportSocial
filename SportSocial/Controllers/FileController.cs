using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Storage;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
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
            var result = _fileService.UploadBlogImage(uploaderHelper.InputStream, uploaderHelper.FileName);
            return Json(result);
        }
	}
}