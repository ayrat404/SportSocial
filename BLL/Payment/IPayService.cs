using DAL.DomainModel.EnumProperties;

namespace BLL.Payment
{
    public interface IPayService
    {
        void InitPay(decimal amount, PayType payType);
    }
}