using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using BLL.Core.Services.Support;
using BLL.Core.Services.Support.Objects;
using BLL.Feedbacks;
using NLog;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    public class HomeController : SportSocialControllerBase
    {
        private readonly ISupportService _supportService;

        public HomeController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Rules()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        [CustomAntiForgeryValidator]
        public ActionResult Feedback(FeedBackModel feedBackModel)
        {
            if (ModelState.IsValid)
            {
                _supportService.Send(feedBackModel);
                return Json(new {Success = true});
            }
            return Json(new {Success = false, ErrorMessage = GetModelStateErrors()});
        }

    }
}