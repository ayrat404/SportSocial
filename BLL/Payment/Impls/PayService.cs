using System.Web;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Payment.Impls
{
    internal class PayService : IPayService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PayService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public void InitPay(int productId, PayType payType, int count = 1)
        {
            var product = _paymentRepository.GetProductById(productId);
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
        }
    }
}