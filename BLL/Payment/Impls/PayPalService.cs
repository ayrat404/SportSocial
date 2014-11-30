using System;
using AutoMapper;
using BLL.Common.Extensions;
using BLL.Common.Objects;
using BLL.Infrastructure.Map;
using BLL.Payment.ViewModels;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Payment.Impls
{
    public class PayPalService : IPayPalService
    {
        private readonly IRepository _repository;
        private readonly IPayService _payService;

        private const string ViewName = "_paypal";

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
            resultModel.SuccessUrl = "http://localhost:53560/pay/PayPalSuccess/"; //"http://fortress.club/";
            resultModel.CancelUrl = "http://localhost:53560/pay/Cancel/";  //""http://fortress.club/";
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
    }
}