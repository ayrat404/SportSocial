using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportSocial.Controllers
{
    public class ConferenceController : Controller
    {
        //
        // GET: /Conference/
        [HttpGet]
        public ActionResult Time()
        {
            if (new Random().Next(2) == 1)
                return Json(new {stamp = TimeSpan.FromDays(23).TotalSeconds}, JsonRequestBehavior.AllowGet);
            return Json(new {url = "/"}, JsonRequestBehavior.AllowGet);
        }
	}
}