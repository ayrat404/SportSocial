using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;

namespace BLL.Payment.Impls
{
    internal class PayService : IPayService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PayService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public void InitPay(decimal amount, PayType payType)
        {
            throw new System.NotImplementedException();
        }
    }
}