using System.Collections.Generic;
using System.Linq;
using DAL.DomainModel;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IRepository _repository;

        public PaymentRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Product GetProductById(int productId)
        {
            return _repository.Find<Product>(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _repository.Queryable<Product>()
                .Where(p => new[] {1, 6, 12}.Contains(p.Id))
                .ToList();
        }

        public void AddPay(Pay pay)
        {
            _repository.Add(pay);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}