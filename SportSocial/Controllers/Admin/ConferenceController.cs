using System;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
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
            var conf = _conferenceService.GetLastConf();
            return View("Partials/ConferenceTimer", conf);
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