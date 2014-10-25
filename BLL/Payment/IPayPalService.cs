using BLL.Payment.ViewModels;

namespace BLL.Payment
{
    public interface IPayPalService
    {
        PayPalResult SetExpressCheckout(int productId);
        PayPalResult ExpressCheckoutDetails(string token, string payerId);
        PayPalResult DoExpressCheckout();
    }
}