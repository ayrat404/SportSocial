using BLL.Common.Objects;
using BLL.Payment.Impls;

namespace BLL.Payment.ViewModels
{
    public class PayResult: ServiceResult
    {
        public PayViewModel PayModel { get; set; }
    }
}