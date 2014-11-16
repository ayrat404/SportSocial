using System;
using System.Globalization;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Payment.ViewModels;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;

namespace BLL.Payment
{
    public interface IRobokassaService
    {
        RobokassaViewModel CreateModel(int payId);
        ServiceResult PaySuccess(RobocassaResultModel successModel);
        ServiceResult PayFail(RobocassaResultModel successModel);
    }

    public class RobokassaService : IRobokassaService
    {
        private readonly IRepository _repository;
        private string _merchantLogn = "FortressSport";
        private string _merchantPasswd = "Qroi23is";

        public RobokassaService(IRepository repository)
        {
            _repository = repository;
        }

        public RobokassaViewModel CreateModel(int payId)
        {
            var pay = _repository.Find<Pay>(payId);
            var robocassaViewModel = new RobokassaViewModel
            {
                Id = pay.Id,
                Cost = pay.Amount.ToString("0.00", CultureInfo.InvariantCulture),
                Description = pay.Comment,
                MerchantLogin = _merchantLogn
            };
            string stringToSign = string.Format("{0}:{1}:{2}:{3}", _merchantLogn, robocassaViewModel.Cost, pay.Id, _merchantPasswd);
            robocassaViewModel.Signature = Hasher.Md5(stringToSign);
            return robocassaViewModel;
        }

        public ServiceResult PaySuccess(RobocassaResultModel successModel)
        {
            var result = new ServiceResult {Success = false};
            var stringToVerify = string.Format("{0}:{1}:{2}", 
                successModel.OutSum,
                successModel.InvId,
                _merchantPasswd
            );
            var hashToVerify = Hasher.Md5(stringToVerify);
            if (!String.Equals(hashToVerify, successModel.SignatureValue, StringComparison.CurrentCultureIgnoreCase))
            {
                result.ErrorMessage = "Хеши не совпадают";
                return result;
            }
            var pay = _repository.Find<Pay>(successModel.InvId);
            if (pay == null)
            {
                result.ErrorMessage = "Не найден платеж";
                return result;
            }
            if (pay.Amount != decimal.Parse(successModel.OutSum, NumberStyles.AllowDecimalPoint))
            {
                result.ErrorMessage = "Суммы не совпадают";
                return result;
            }
            //CultureInfo ci = C
            pay.PaySatus = PaySatus.Completed;
            _repository.Update(pay);
            result.Success = true;
            return result;
        }

        public ServiceResult PayFail(RobocassaResultModel successModel)
        {
            var pay = _repository.Find<Pay>(successModel.InvId);
            if (pay != null)
            {
                pay.PaySatus = PaySatus.Failured;
                _repository.Update(pay);
                _repository.SaveChanges();
            }
            return new ServiceResult {Success = true};
        }
    }
}