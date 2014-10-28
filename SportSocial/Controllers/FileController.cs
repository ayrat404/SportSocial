using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Storage;

namespace SportSocial.Controllers
{
    public class FileController : Controller
    {
        //private readonly IFileService _fileService;

        //public FileController(IFileService fileService)z
        //{
        //    _fileService = fileService;
        //}

        [HttpPost]
        public ActionResult Images(HttpPostedFileBase image)
        {
            return Json(new {success = true, id = 3288388, url = "/storage/images/1.jpg"});
            //var result = _fileService.UploadBlogImage(image.InputStream, image.FileName);
            //return Json(result);
        }
	}
}