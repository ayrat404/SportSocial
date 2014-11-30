using BLL.Common.Objects;
using BLL.Payment.ViewModels;
using DAL.DomainModel.EnumProperties;

namespace BLL.Payment
{
    public interface IPayService
    {
        PayResult InitPay(PayType payType, int productId, int count = 1);
        ServiceResult Cancel();
    }
}