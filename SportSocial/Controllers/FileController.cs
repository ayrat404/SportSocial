using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportSocial.Controllers
{
    public class FileController : Controller
    {
        [HttpPost]
        public ActionResult Images()
        {
            return Json(new {success = true, id = 3288388, url = "/storage/images/1.jpg"});
        }
	}
}