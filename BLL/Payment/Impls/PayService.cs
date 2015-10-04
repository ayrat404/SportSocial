using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BLL.Common.Extensions;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Payment.ViewModels;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;

namespace BLL.Payment.Impls
{
    public class PayService : IPayService
    {
        private const int TrialDays = 15;

        private readonly IPaymentRepository _paymentRepository;
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public PayService(IPaymentRepository paymentRepository, IRepository repository, ICurrentUser currentUser)
        {
            _paymentRepository = paymentRepository;
            _repository = repository;
            _currentUser = currentUser;
        }

        public PayResult InitPay(PayType payType, int productId, int count)
        {
            var result = new PayResult {Success = true};
            var product = _paymentRepository.GetProductById(productId);
            if (product == null)
            {
                result.Success = false;
                return result;
            }
            var cost = product.Cost*product.Count;
            var pay = new Pay
            {
                ProductId = productId,
                Amount = cost,
                PaySatus = PaySatus.Created,
                PayType = payType,
                UserId = _currentUser.UserId,
                ProductCount = product.Count,
                Comment = product.Label,
            };
            _paymentRepository.AddPay(pay);
            _paymentRepository.SaveChanges();
            result.PayModel = new PayViewModel
            {
                Cost = pay.Amount.ToStringWithDot(),
                CostMonth = product.Cost.ToStringWithDot(),
                Id = pay.Id,
                Description = pay.Comment,
                Currency = product.Currency
            };
            return result;
        }

        public ServiceResult CompletePay(Pay pay)
        {
            var result = new ServiceResult() {Success = false};
            if (pay == null)
            {
                return result;
            }
            pay.PaySatus = PaySatus.Completed;
            pay.User.Profile.LastPaymentDate = DateTime.Now;
            pay.User.Profile.LastPaidDaysCount = (DateTime.Now.AddMonths(pay.ProductCount) - DateTime.Now).Days;
            pay.User.Profile.IsTrial = false;
            pay.User.Profile.IsPaid = true;
            _repository.SaveChanges();
            result.Success = true;
            return result;
        }

        public ServiceResult CompletePay(int payId)
        {
            var pay = _repository.Find<Pay>(payId);
            return CompletePay(pay);
        }

        public ServiceResult Cancel()
        {
            var lastPay = _repository
                .Queryable<Pay>()
                .Where(p => p.UserId == _currentUser.UserId)
                .OrderByDescending(p => p.Created)
                .Take(1)
                .SingleOrDefault();
            return Cancel(lastPay);
        }

        public ServiceResult Cancel(Pay pay)
        {
            var result = new ServiceResult
            {
                Success = true
            };
            if (pay == null)
                return result;
            if (pay.PaySatus == PaySatus.Created)
            {
                pay.PaySatus = PaySatus.Canceled;
                _repository.SaveChanges();
            }
            return result;
        }

        public ServiceResult Cancel(int payId)
        {
            return Cancel(_repository.Find<Pay>(payId));
        }

        public IEnumerable<Product> GetProducts()
        {
            return _paymentRepository.GetProducts();
        }

        public PayViewModel GetPayInfo(int payId)
        {
            var pay = _repository.Find<Pay>(payId);
            return GetPayInfo(pay);
        }

        public PayViewModel GetPayInfo(Pay pay)
        {
            return new PayViewModel
            {
                Cost = pay.Amount.ToStringWithDot(),
                Id = pay.Id,
                Description = pay.Comment,
                Currency = pay.Product.Currency
            };
        }

        private Pay LastPay(AppUser user)
        {
            return user.Pays
                .Where(p => p.PaySatus == PaySatus.Completed)
                .OrderByDescending(p => p.Created)
                .FirstOrDefault();
        }

        public PaymentStatusVm GetPaymentStatus()
        {
            var user = _repository.Queryable<AppUser>()
                .Where(u => u.Id == _currentUser.UserId)
                .Include(u => u.Pays)
                .Include(u => u.Pays.Select(p => p.Product))
                .Single();

            PaymentStatusVm paymentStatus = new PaymentStatusVm();
            var profile = _currentUser.User.Profile;
            paymentStatus.Payment = new PaymentStatus()
            {
                Status = profile.HasSubscription(),
                Until = profile.DateUntil()
            };
            paymentStatus.Systems = new List<PayTypeVm>
            {
                new PayTypeVm()
                {
                    Id = 1,
                    Name = "Робокасса"
                }
            };
            paymentStatus.Tariffs = _paymentRepository.GetProducts()
                .Select(p => new TarifVm()
                {
                    Id = p.Id,
                    Cost = p.Cost,
                    Curr = p.Currency,
                    Month = p.Count
                }).ToList();

            paymentStatus.Trial = new TrialInfo()
            {
                AllDays = TrialDays,
                CurrentDays = profile.IsTrial ? (profile.DateUntil() - DateTime.Now).Value.Days : 0
            };

            return paymentStatus;
        }

        //public ServiceResult InitSocialPay(int productId, int payTypeId)
        //{
        //    var initResult = InitPay((PayType)payTypeId, productId, 1);
        //    _r
        //}
    }
}