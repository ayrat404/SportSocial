using System.Web.Mvc;
using BLL.Payment;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class PayController : SportSocialControllerBase
    {

        private readonly IPayPalService _payPalService;

        //public PayController(IPayPalService payPalService)
        //{
        //    _payPalService = payPalService;
        //}

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Paypal(string amount)
        {
            //var result = _payPalService.SetExpressCheckout(amount);
            //if (result.Success)
            //{
            //    return Redirect(result.RedirectUrl);
            //}
            //return RedirectToAction("Index");
            return View();
        }

        [HttpGet]
        public ActionResult PayPalConfirm(string token, string payerId)
        {
            var result = _payPalService.ExpressCheckoutDetails(token, payerId);
            return View();
        }

        [HttpGet]
        public ActionResult PayPalConfirm()
        {
            var result = _payPalService.DoExpressCheckout();
            return View();
        }
    }
}