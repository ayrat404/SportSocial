using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Login;
using BLL.Login.ViewModels;
using SportSocial.Controllers.Base;
using WebGrease.Css.Extensions;

namespace SportSocial.Controllers
{
    public class SettingsController : SportSocialControllerBase
    {
        
        
        public ActionResult Index()
        {
            return View();
        }

    }
}