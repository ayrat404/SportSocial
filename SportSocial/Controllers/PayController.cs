using System.Web.Mvc;
using BLL.Payment;
using BLL.Payment.ViewModels;
using DAL.DomainModel.EnumProperties;
using NLog;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class PayController : SportSocialControllerBase
    {
        private readonly IPayPalService _payPalService;
        private readonly IPayService _payService;
        private readonly IRobokassaService _robokassaService;
        private static Logger _logger = LogManager.GetCurrentClassLogger();


        public PayController(IPayPalService payPalService, IPayService payService, IRobokassaService robokassaService)
        {
            _payPalService = payPalService;
            _payService = payService;
            _robokassaService = robokassaService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PayType payType, int product, int count = 1)
        {
            var payInfo = _payService.InitPay(payType, product, count);
            PayViewModel model;
            switch (payType)
            {
                case PayType.Robokassa:
                    model = _robokassaService.CreateModel(payInfo.PayModel.Id);
                    break;
                case PayType.PayPal:
                    model = _payPalService.CreateModel(payInfo.PayModel);
                    break;
                default:
                    return View("index");
            }
            return View("confirm", model);
        }

        [HttpGet]
        public ActionResult RobokassaSuccess(string invId, string outSum, string signatureValue, string culture)
        {
            //var successModel = new RobocassaResultModel
            //{
            //    InvId = invId,
            //    OutSum = outSum,
            //    SignatureValue = signatureValue
            //};
            //var result = _robokassaService.PaySuccess(successModel);
            return View("success");
        }

        [HttpGet]
        public ActionResult RobokassaFail(string outSum, string invId, string signatureValue)
        {
            var successModel = new RobocassaResultModel
            {
                InvId = invId,
                OutSum = outSum,
                SignatureValue = signatureValue
            };
            _payService.Cancel(int.Parse(invId));
            return View("cancel");
        }

        [HttpPost]
        public ActionResult RobokassaResult(string outSum, string invId, string signatureValue)
        {
            _logger.Info("RobokassaResult|outSum={0} invId={1} signatureValue={2}", outSum, invId, signatureValue);
            var successModel = new RobocassaResultModel
            {
                InvId = invId,
                OutSum = outSum,
                SignatureValue = signatureValue
            };
            var result = _robokassaService.PaySuccess(successModel);
            return Content(result.Response);
        }

        //[HttpPost]
        //public ActionResult Paypal(string amount)
        //{
        //    //var result = _payPalService.SetExpressCheckout(amount);
        //    //if (result.Success)
        //    //{
        //    //    return Redirect(result.RedirectUrl);
        //    //}
        //    //return RedirectToAction("Index");
        //    return View();
        //}

        [HttpPost]
        public ActionResult PayPalSuccess(PayPalSuccesModel payPalSucces)
        {
            var result = _payPalService.Succes(payPalSucces);
            if (result.Success)
            {
                return View("success");
            }
            return View("error", result);
        }

        [HttpGet]
        public ActionResult Cancel()
        {
            _payService.Cancel();
            return View();
        }
    }
}