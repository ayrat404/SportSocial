using System;
using System.Web.Mvc;
using BLL.Admin.Conference;
using BLL.Admin.Conference.ViewModels;
using BLL.Admin.Moderation.ViewModels;

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

        [HttpGet]
        public ActionResult Index()
        {
            var model = _conferenceService.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateConfModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateConfModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Edit(EditConfModel model)
        {
            throw new NotImplementedException();
        }
	}
}