namespace BLL.Common.Objects
{
    public class HasDate
    {
        public string Date { get; set; }
    }

    public interface IHasDate
    {
        string Date { get; set; }
    }
}