﻿using System;
using System.Web.Mvc;
using BLL.Common.Objects;
using BLL.Storage;
using BLL.Storage.Objects;
using BLL.Storage.Objects.Enums;
using Knoema.Localization;
using NLog;
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
        [CustomAntiForgeryValidator]
        public JsonResult YoutubeUrl(string youtubeUrl)
        {
            return Json(_fileService.YoutubeImage(youtubeUrl));
        }

        [HttpPost]
        //[CustomAntiForgeryValidator]
        public ActionResult Images(UploadImageModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _fileService.UploadImage(model.Image.InputStream, model.Image.FileName, model.Type);
                return Json(result);
            }
            return Json(new {Success = false});
        }

        [HttpPost]
        //[CustomAntiForgeryValidator]
        public ActionResult UploadImage(UploadImageModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Type)
                {
                    case UploadType.Avatar:
                        return Json(_fileService.UploadAvatar(model.Image.InputStream, model.Image.FileName));
                    case UploadType.Article:
                        return Json(_fileService.UploadImage(model.Image.InputStream, model.Image.FileName, UploadType.Article));
                    case UploadType.Album:
                        throw new NotImplementedException();
                }
            }
            return Json(new ServiceResult {Success = false, ErrorMessage = "Ошибка".Resource(this)});
        }
	}
}