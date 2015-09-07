using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.EnumProperties;

namespace DAL.DomainModel.Interfaces
{
    public class RatingEntityBase: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public RatingType RatingType { get; set; }

        public int RatedEntityId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }

    public class RatingEntity<THasRatingEntity>: RatingEntityBase  where THasRatingEntity : class
    {
        [ForeignKey("RatedEntityId")]
        public THasRatingEntity RatedEntity { get; set; }
    }
}