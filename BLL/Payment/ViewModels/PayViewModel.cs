namespace BLL.Payment.ViewModels
{
    public class PayViewModel
    {
        public int Id { get; set; }
        public string Cost { get; set; }
        public string CostMonth { get; set; }
        public string Description { get; set; }
        public string ViewName { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        public string NotificationUrl { get; set; }
        public string Currency { get; set; }
    }
}