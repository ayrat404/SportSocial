namespace BLL.Payment.ViewModels
{
    public class PayPalIpnModel
    {
        public string Item_name { get; set; }
        public string Item_number { get; set; }
        public string Mc_currency { get; set; }
        public string Mc_gross { get; set; }
        public string Payment_status { get; set; }
        public string Payment_fee { get; set; }
        public string Auth_status { get; set; }
        public string Payer_status { get; set; }
        public string Pending_reason { get; set; }
        public string Reason_code { get; set; }
    }
}