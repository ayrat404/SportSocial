using System.Threading.Tasks;
using BLL.Common.Objects;
using BLL.Payment.ViewModels;

namespace BLL.Payment
{
    public interface IPayPalService
    {
        PayPalModel CreateModel(int payId);
        ServiceResult Succes(PayPalSuccesModel payPalSucces);
        void Ipn(PayPalIpnModel ipnModel, string queryString);
    }
}