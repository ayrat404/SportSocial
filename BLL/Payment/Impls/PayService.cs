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
                UserId = HttpContext.Current.User.Identity.GetUserId(),
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

        public ServiceResult Cancel()
        {
            var result = new ServiceResult
            {
                Success = true
            };
            var lastPay = _repository
                .Queryable<Pay>()
                .Where(p => p.UserId == _currentUser.UserId)
                .OrderByDescending(p => p.Created)
                .Take(1)
                .SingleOrDefault();
            if (lastPay == null)
                return result;
            if (lastPay.PaySatus == PaySatus.Created)
            {
                lastPay.PaySatus = PaySatus.Canceled;
                _repository.SaveChanges();
            }
            return result;
        }
    }
}