using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Admin.Users;

namespace SportSocial.Areas.root.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserManagmentService _userseService;

        public UsersController(IUserManagmentService userseService)
        {
            _userseService = userseService;
        }

        //
        // GET: /root/Users/
        public ActionResult GetUsers()
        {
            return View();
        }
	}
}