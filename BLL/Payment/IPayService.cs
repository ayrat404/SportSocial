using BLL.Common.Objects;
using BLL.Payment.Impls;
using BLL.Payment.ViewModels;
using DAL.DomainModel.EnumProperties;

namespace BLL.Payment
{
    public interface IPayService
    {
        PayResult InitPay(int productId, PayType payType, int count = 1);
    }
}