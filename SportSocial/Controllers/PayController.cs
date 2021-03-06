﻿using System.Threading.Tasks;
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
        [AllowAnonymous]
        public ActionResult Index()
        {
            return Redirect("//social.fortress.club/payment");
            //return View();
        }

        [HttpPost]
        [CustomAntiForgeryValidator]
        public ActionResult Index(int product, int count = 1)
        {
            var payInfo = _payService.InitPay(PayType.Init, product, count);
            return RedirectToAction("Confirm", new {id = payInfo.PayModel.Id});
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            var payVm = _robokassaService.CreateModel(id);
            return View(payVm);
        }

        [HttpPost]
        public ActionResult Html(PayType type, int payId)
        {
            PayViewModel model = null;
            switch (type)
            {
                case PayType.PayPal:
                    model = _payPalService.CreateModel(payId);
                    break;
                case PayType.Robokassa:
                    model = _robokassaService.CreateModel(payId);
                    break;
            }
            if (model == null)
                return HttpNotFound();
            return PartialView("partials/" + model.ViewName, model);
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
        //[HttpGet]
        [AllowAnonymous]
        public ActionResult RobokassaResult(string outSum, string invId, string signatureValue)
        {
            _logger.Info("Robokassa form: {0}", Request.Form.ToString());
            //_logger.Info("RobokassaResult|outSum={0} invId={1} signatureValue={2}", outSum, invId, signatureValue);
            var successModel = new RobocassaResultModel
            {
                InvId = invId,
                OutSum = outSum,
                SignatureValue = signatureValue
            };
            var result = _robokassaService.PaySuccess(successModel);
            return Content(result.Response);
        }

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

        [HttpPost]
        [AllowAnonymous]
        public void PayPalIpn(PayPalIpnModel ipnModel)
        {
            string queryString = Request.Form.ToString();
            _logger.Info(queryString);
            //PayPalIpnModel ipnModel = new PayPalIpnModel
            //{
            //    Payment_status = Payment_status,
            //    Item_name = Item_name,
            //    Item_number = Item_number,
            //    Mc_currency = Mc_currency,
            //    Mc_gross = Mc_gross
            //};
            _logger.Info("{0}, {1}, {2}, {3}, {4}", ipnModel.Item_name, ipnModel.Item_number, ipnModel.Mc_currency,
                ipnModel.Mc_gross, ipnModel.Payment_status);
            Task.Run(() => _payPalService.Ipn(ipnModel, queryString));
        }

        [HttpGet]
        public ActionResult Cancel()
        {
            _payService.Cancel();
            return View();
        }
    }
}