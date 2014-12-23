using System;
using System.Web.Mvc;
using BLL.Admin.Conference;
using BLL.Admin.Conference.ViewModels;
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
        public ActionResult Item(int id)
        {
            var conf = _conferenceService.GetInProcessConf(id);
            return View(conf);
        }

        [HttpGet]
        public ActionResult History()
        {
            var hostiryCOnfModel = new ConferenceHostiryModel
            {
                Current = _conferenceService.GetLastConf(),
                Hostiry = _conferenceService.GetAll(),
            };
            return View(hostiryCOnfModel);
        }
	}
}