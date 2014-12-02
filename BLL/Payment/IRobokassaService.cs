using System;
using System.Globalization;
using BLL.Common.Extensions;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Infrastructure.Map;
using BLL.Payment.ViewModels;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using Knoema.Localization;
using NLog;

namespace BLL.Payment
{
    public interface IRobokassaService
    {
        RobokassaViewModel CreateModel(int payId);
        RobokassaSuccessResult PaySuccess(RobocassaResultModel successModel);
        ServiceResult PayFail(RobocassaResultModel successModel);
    }

    public class RobokassaService : IRobokassaService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IRepository _repository;
        private readonly IPayService _payService;
        private const string MerchantLogn = "FortressSport";
        private const string MerchantPasswd = "Qroi23is";
        private const string ViewName = "_robokassa";

        public RobokassaService(IRepository repository, IPayService payService)
        {
            _repository = repository;
            _payService = payService;
        }

        public RobokassaViewModel CreateModel(int payId)
        {
            var pay = _repository.Find<Pay>(payId);
            var robocassaViewModel = new RobokassaViewModel
            {
                Id = pay.Id,
                Cost = pay.Amount.ToString("0.00", CultureInfo.InvariantCulture),
                Description = pay.Comment,
                MerchantLogin = MerchantLogn,
                ViewName = ViewName,
            };
            string stringToSign = string.Format("{0}:{1}:{2}:{3}", MerchantLogn, robocassaViewModel.Cost, pay.Id, MerchantPasswd);
            robocassaViewModel.Signature = Hasher.Md5(stringToSign);
            return robocassaViewModel;
        }

        public RobokassaSuccessResult PaySuccess(RobocassaResultModel successModel)
        {
            var result = new RobokassaSuccessResult {Success = false};
            var stringToVerify = string.Format("{0}:{1}:{2}", 
                successModel.OutSum,
                successModel.InvId,
                MerchantPasswd);
            var hashToVerify = Hasher.Md5(stringToVerify);
            if (!String.Equals(hashToVerify, successModel.SignatureValue, StringComparison.CurrentCultureIgnoreCase))
            {
                _logger.Info("PaySuccess| Hashs not match");
                result.ErrorMessage = "Хеши не совпадают".Resource(this);
                return result;
            }
            var pay = _repository.Find<Pay>(int.Parse(successModel.InvId));
            if (pay == null)
            {
                _logger.Info("PaySuccess| Pay not found");
                result.ErrorMessage = "Не найден платеж".Resource(this);
                return result;
            }
            if (pay.Amount.ToStringWithDot() != successModel.OutSum)
            {
                _logger.Info("PaySuccess| Sums not match");
                result.ErrorMessage = "Суммы не совпадают".Resource(this);
                return result;
            }
            //CultureInfo ci = C
            var resultPay = _payService.CompletePay(pay);
            if (resultPay.Success)
            {
                result.Success = true;
                result.Response = string.Format("OK{0}", successModel.InvId);
                return result;
            }
            _logger.Info("PaySuccess| CompletePay success false");
            return result;
        }

        public ServiceResult PayFail(RobocassaResultModel successModel)
        {
            var pay = _repository.Find<Pay>(int.Parse(successModel.InvId));
            if (pay != null)
            {
                pay.PaySatus = PaySatus.Failured;
                _repository.Update(pay);
                _repository.SaveChanges();
            }
            return new ServiceResult {Success = true};
        }
    }

    public class RobokassaSuccessResult: ServiceResult
    {
        public string Response { get; set; }
    }
}