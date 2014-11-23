using System;
using System.Web.Mvc;
using BLL.Admin.Conference;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class ConferenceController : SportSocialControllerBase
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
            var conf = _conferenceService.GetLastConf();
            if (conf != null)
                conf.Stamp = (int)(conf.Date - DateTime.Now).TotalSeconds;
            return Json(conf, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RenderTime()
        {
            var conf = _conferenceService.GetLastConf();
            if (conf != null)
                conf.Stamp = (int)(conf.Date - DateTime.Now).TotalMilliseconds;
            return View("Partials/ConferenceTimer", conf);
        }

        [HttpGet]
        public ActionResult Index(int id)
        {
            var conf = _conferenceService.GetInProcessConf(id);
            return View(conf);
        }

        //[HttpGet]
        //public ActionResult Item(int id)
        //{
        //    var conf = _conferenceService.GetInProcessConf(id);
        //    return Json(conf, JsonRequestBehavior.AllowGet);
        //}

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