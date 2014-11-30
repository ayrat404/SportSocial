namespace BLL.Payment.ViewModels
{
    public class PayPalSuccesModel
    {
        public int Item_number { get; set; }
        public string Mc_currency { get; set; }
        public string Payment_status { get; set; }
        public string Payment_gross { get; set; }
        public string Mc_gross { get; set; }
    }
}