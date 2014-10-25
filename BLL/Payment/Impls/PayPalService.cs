using BLL.Payment.ViewModels;
using PayPal.SOAP;

namespace BLL.Payment.Impls
{
    class PayPalService : IPayPalService
    {
        private readonly IPayService _payService;

        public PayPalService(IPayService payService)
        {
            _payService = payService;
        }

        public PayPalResult SetExpressCheckout(int productId)
        {
            throw new System.NotImplementedException();
        }

        public PayPalResult ExpressCheckoutDetails(string token, string payerId)
        {
            throw new System.NotImplementedException();
        }

        public PayPalResult DoExpressCheckout()
        {
            throw new System.NotImplementedException();
        }
    }
}