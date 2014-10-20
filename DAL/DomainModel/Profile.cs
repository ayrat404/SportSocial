using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DomainModel
{
    public class Profile: ICutrureSpecific
    {
        [ForeignKey("AppUser")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string Lang { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}