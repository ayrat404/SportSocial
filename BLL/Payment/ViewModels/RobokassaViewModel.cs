namespace BLL.Payment.ViewModels
{
    public class RobokassaViewModel: PayViewModel
    {
        public string Signature { get; set; }
        public string MerchantLogin { get; set; }
    }
}