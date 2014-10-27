using DAL.DomainModel.Base;

namespace DAL.DomainModel
{
    public class Product: IEntity, ICultrureSpecific
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public decimal Cost { get; set; }

        public string Currency { get; set; }

        public string Lang { get; set; }
    }
}