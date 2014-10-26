using DAL.DomainModel.EnumProperties;

namespace BLL.Payment
{
    public interface IPayService
    {
        void InitPay(int productId, PayType payType, int count = 1);
    }
}