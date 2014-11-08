using System.ComponentModel.DataAnnotations;
using BLL.Blog.Enums;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.ViewModels
{
    public class BlogRatingViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public RatingType ActionType { get; set; }

        [Required]
        public RatingEntityType EntityType { get; set; }
    }
}