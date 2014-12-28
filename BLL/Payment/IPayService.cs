using System.Collections.Generic;
using BLL.Common.Objects;
using BLL.Payment.ViewModels;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;

namespace BLL.Payment
{
    public interface IPayService
    {
        PayResult InitPay(PayType payType, int productId, int count = 1);
        ServiceResult CompletePay(Pay pay);
        ServiceResult CompletePay(int payId);
        ServiceResult Cancel();
        ServiceResult Cancel(Pay pay);
        ServiceResult Cancel(int payId);
        IEnumerable<Product> GetProducts();
    }
}