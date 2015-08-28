using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public ActionResult Index(string path)
        {
            return View("~/views/template/"+ path + ".cshtml");
        }
    }
}