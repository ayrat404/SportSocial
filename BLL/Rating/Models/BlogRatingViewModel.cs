using System.ComponentModel.DataAnnotations;
using BLL.Blog.Enums;
using BLL.Rating.Enums;
using DAL.DomainModel.EnumProperties;

namespace BLL.Rating.Models
{
    public class RatingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public RatingType ActionType { get; set; }

        [Required]
        public RatingEntityType EntityType { get; set; }
    }
}