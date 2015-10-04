using System;
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
        PayViewModel GetPayInfo(int payId);
        PayViewModel GetPayInfo(Pay pay);
        PaymentStatusVm GetPaymentStatus();
        //ServiceResult InitSocialPay(int productId, int payTypeId);
    }

    public class PaymentStatusVm
    {
        public TrialInfo Trial { get; set; }

        public List<TarifVm> Tariffs { get; set; }

        public List<PayTypeVm> Systems { get; set; }

        public PaymentStatus Payment { get; set; }
    }

    public class PaymentStatus
    {
        public bool Status { get; set; }
        public DateTime? Until { get; set; }
    }

    public class PayTypeVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TarifVm
    {
        public int Id { get; set; } 
        public int Month { get; set; } 
        public decimal Cost { get; set; } 
        public string Curr { get; set; } 
    }



    public class TrialInfo
    {
        public int CurrentDays { get; set; }
        public int AllDays { get; set; }
    }
}