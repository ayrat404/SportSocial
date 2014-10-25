using BLL.Common.Objects;

namespace BLL.Payment.ViewModels
{
    public class PayPalResult: ServiceResult
    {
        public string RedirectUrl { get; set; }
    }
}
