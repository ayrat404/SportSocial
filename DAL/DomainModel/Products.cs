using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel
{
    public class Product: IEntity, ICultrureSpecific
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Label { get; set; }

        public decimal Cost { get; set; }

        public int Count { get; set; }

        public string Currency { get; set; }

        public string Lang { get; set; }
    }
}