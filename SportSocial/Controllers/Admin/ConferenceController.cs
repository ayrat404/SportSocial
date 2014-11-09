using System;
using System.Web.Mvc;
using BLL.Admin.Conference;
using BLL.Admin.Conference.ViewModels;

namespace SportSocial.Controllers
{
    [Authorize]
    public class ConferenceController : Controller
    {
        private readonly IConferenceService _conferenceService;

        public ConferenceController(IConferenceService conferenceService)
        {
            _conferenceService = conferenceService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Time()
        {
            if (new Random().Next(2) == 1)
                return Json(new {stamp = TimeSpan.FromDays(23).TotalSeconds}, JsonRequestBehavior.AllowGet);
            return Json(new {url = "/"}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    var model = _conferenceService.GetAll();
        //    return View(model);
        //}

        //[HttpPost]
        //public JsonResult Create(CreateConfModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return Json(new {success = false});
        //    _conferenceService.Create(model);
        //    return Json(new {success = true});
        //}

        //[HttpPost]
        //public ActionResult Edit(ConfModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return Json(new {success = false});
        //    _conferenceService.Edit(model);
        //    return Json(new {success = true});
        //}
	}
}