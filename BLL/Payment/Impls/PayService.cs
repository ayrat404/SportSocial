using System.Web;
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

        public PayService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public PayResult InitPay(int productId, PayType payType, int count = 1)
        {
            var result = new PayResult() {Success = true};
            var product = _paymentRepository.GetProductById(productId);
            if (product == null)
            {
                result.Success = false;
                return result;
            }
            var cost = product.Cost*count;
            var pay = new Pay()
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
                Cost = pay.Amount.ToString(),
                Id = pay.Id,
                Description = pay.Comment
            };
            return result;
        }
    }
}