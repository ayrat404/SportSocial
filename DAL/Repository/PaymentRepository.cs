using DAL.DomainModel;
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