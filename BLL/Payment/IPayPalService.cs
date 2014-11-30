using BLL.Common.Objects;
using BLL.Payment.ViewModels;

namespace BLL.Payment
{
    public interface IPayPalService
    {
        PayPalModel CreateModel(PayViewModel payModel);
        ServiceResult Succes(PayPalSuccesModel payPalSucces);
    }
}