using System.Web.Mvc;
using BLL.Payment;
using BLL.Payment.ViewModels;
using DAL.DomainModel.EnumProperties;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    //[Authorize]
    public class PayController : SportSocialControllerBase
    {
        private readonly IPayPalService _payPalService;
        private readonly IPayService _payService;
        private readonly IRobokassaService _robokassaService;

        public PayController(IPayPalService payPalService, IPayService payService, IRobokassaService robokassaService)
        {
            _payPalService = payPalService;
            _payService = payService;
            _robokassaService = robokassaService;
        }

        public ActionResult Index(int productId, PayType payType)
        {
            var payInfo = _payService.InitPay(productId, PayType.Robokassa);
            var model = _robokassaService.CreateModel(payInfo.PayModel.Id);
            return View(model);
        }

        [HttpGet]
        public ActionResult RobokassaSuccess(int InvId, string OutSum, string SignatureValue, string Culture)
        {
            RobocassaResultModel successModel = new RobocassaResultModel
            {
                InvId = InvId,
                OutSum = OutSum,
                SignatureValue = SignatureValue
            };
            var result = _robokassaService.PaySuccess(successModel);
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