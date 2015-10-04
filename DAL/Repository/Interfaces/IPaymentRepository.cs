using System.Collections.Generic;
using DAL.DomainModel;

namespace DAL.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        Product GetProductById(int productId);
        void AddPay(Pay pay);
        void SaveChanges();
        IEnumerable<Product> GetProducts();
    }
}