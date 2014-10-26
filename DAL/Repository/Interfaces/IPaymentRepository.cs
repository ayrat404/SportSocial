using DAL.DomainModel;

namespace DAL.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        Product GetProductById(int productId);
        void AddPay(Pay pay);
    }
}