namespace BLL.Payment.ViewModels
{
    public class RobocassaResultModel
    {
        public int InvId { get; set; }
        public string OutSum { get; set; }
        public string SignatureValue { get; set; }
        public string Culture { get; set; }
    }
}