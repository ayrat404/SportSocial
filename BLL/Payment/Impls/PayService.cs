using System.Globalization;
using System.Linq;
using System.Web;
using BLL.Common.Extensions;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using BLL.Payment.ViewModels;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Payment.Impls
{
    public class PayService : IPayService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public PayService(IPaymentRepository paymentRepository, IRepository repository, ICurrentUser currentUser)
        {
            _paymentRepository = paymentRepository;
            _repository = repository;
            _currentUser = currentUser;
        }

        public PayResult InitPay(PayType payType, int productId, int count)
        {
            var result = new PayResult {Success = true};
            var product = _paymentRepository.GetProductById(productId);
            if (product == null)
            {
                result.Success = false;
                return result;
            }
            var cost = product.Cost*count;
            var pay = new Pay
            {
                ProductId = productId,
                Amount = cost,
                PaySatus = PaySatus.Created,
                PayType = payType,
                UserId = _currentUser.UserId,
                ProductCount = count,
                Comment = product.Label,
            };
            _paymentRepository.AddPay(pay);
            _paymentRepository.SaveChanges();
            result.PayModel = new PayViewModel
            {
                Cost = pay.Amount.ToStringWithDot(),
                Id = pay.Id,
                Description = pay.Comment,
                Currency = product.Currency
            };
            return result;
        }

        public ServiceResult CompletePay(Pay pay)
        {
            var result = new ServiceResult() {Success = false};
            if (pay == null)
            {
                return result;
            }
            pay.PaySatus = PaySatus.Completed;
            pay.User.Profile.IsPaid = true;
            _repository.SaveChanges();
            result.Success = true;
            return result;
        }

        public ServiceResult CompletePay(int payId)
        {
            var pay = _repository.Find<Pay>(payId);
            return CompletePay(pay);
        }

        public ServiceResult Cancel()
        {
            var lastPay = _repository
                .Queryable<Pay>()
                .Where(p => p.UserId == _currentUser.UserId)
                .OrderByDescending(p => p.Created)
                .Take(1)
                .SingleOrDefault();
            return Cancel(lastPay);
        }

        public ServiceResult Cancel(Pay pay)
        {
            var result = new ServiceResult
            {
                Success = true
            };
            if (pay == null)
                return result;
            if (pay.PaySatus == PaySatus.Created)
            {
                pay.PaySatus = PaySatus.Canceled;
                _repository.SaveChanges();
            }
            return result;
        }

        public ServiceResult Cancel(int payId)
        {
            return Cancel(_repository.Find<Pay>(payId));
        }
    }
}