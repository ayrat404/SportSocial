using System;
using System.Threading;
using AutoMapper;
using BLL.Common.Extensions;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Infrastructure.Map;
using BLL.Payment.ViewModels;
using DAL;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using Knoema.Localization;
using NLog;

namespace BLL.Payment.Impls
{
    public class PayPalService : IPayPalService
    {
        private readonly IRepository _repository;
        private readonly IPayService _payService;

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private const string ViewName = "_paypal";
        private const string PayPalUrl = "https://www.paypal.com/cgi-bin/webscr/";
        private const string PayPalSanBoxUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr/";

        public PayPalService(IRepository repository, IPayService payService)
        {
            _repository = repository;
            _payService = payService;
        }

        public PayPalModel CreateModel(PayViewModel payModel)
        {
            Mapper.CreateMap<PayViewModel, PayPalModel>();
            var resultModel = payModel.MapTo<PayPalModel>();
            resultModel.ViewName = ViewName;
            resultModel.Business = "ayrat404-facilitator@gmail.com";
            resultModel.SuccessUrl = "http://fortress.club/pay/PayPalSuccess/"; //"http://fortress.club/";
            resultModel.CancelUrl = "http://fortress.club/pay/Cancel/";  //""http://fortress.club/";
            resultModel.NotificationUrl = "http://fortress.club/";
            return resultModel;
        }

        public ServiceResult Succes(PayPalSuccesModel payPalSucces)
        {
            var result = new ServiceResult {Success = false};
            if (payPalSucces.Payment_status.ToLower() != "completed")
            {
                result.ErrorMessage = "Платеж не завершен".Resource(this);
                return result;
            }
            var pay = _repository.Find<Pay>(payPalSucces.Item_number);
            if (pay == null)
            {
                result.ErrorMessage = "Платеж не найден".Resource(this);
                return result;
            }
            if (pay.Amount.ToStringWithDot() == payPalSucces.Payment_gross 
                && String.Equals(pay.Product.Currency, payPalSucces.Mc_currency, StringComparison.CurrentCultureIgnoreCase))
            {
                return _payService.CompletePay(pay);
            }
            result.ErrorMessage = "Ошибка совершения платежа".Resource(this);
            return result;
        }

        public void Ipn(PayPalIpnModel ipnModel, string queryString)
        {
            string payPalResponse = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    string cmsQuery = "cmd=_notify-validate&" + queryString;
                    payPalResponse = HttpHelper.Get(PayPalSanBoxUrl, queryString);
                    break;
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
            if (payPalResponse != "VERIFIED")
            {
                _logger.Error("Respone from paypal: {0}", payPalResponse);
                return;
            }

            if (ipnModel.Payment_status.ToLower() == "completed")
            {
                try
                {
                    Pay pay;
                    using (var db = new EntityDbContext())
                    {
                        pay = db.Pays.Find(int.Parse(ipnModel.Item_number));
                        if (pay != null)
                        {
                            pay.PaySatus = PaySatus.Completed;
                            pay.User.Profile.IsPaid = true;
                            if (ipnModel.Mc_gross == pay.Amount.ToStringWithDot() && ipnModel.Mc_currency == pay.Product.Currency)
                            {
                                //_payService.CompletePay(pay);
                                pay.PaySatus = PaySatus.Completed;
                                pay.User.Profile.IsPaid = true;
                                db.SaveChanges();
                            }
                            else
                            {
                                _logger.Error("Result from PayPal not equal pay: {0} {1}", ipnModel.Mc_currency, ipnModel.Mc_gross);
                            }
                        }
                        else
                        {
                            _logger.Error("pay not found {0}", ipnModel.Item_number);
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                }
                return;
            }
            if (ipnModel.Payment_status.ToLower() == "processed")
            {
                _logger.Info("Pay yet proccess: {0}", ipnModel.Item_number);
            }
            else
            {
                _logger.Error("Pay status is not completed: {0}", ipnModel.Payment_status);
            }
            _logger.Info("end");
        }
    }
}