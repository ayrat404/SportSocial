using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.BlogEntities
{
    public class BlogCommentRating: IEntity, IRatingEntity<BlogComment>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int RatedEntityId { get; set; }
        
        public RatingType RatingType { get; set; }

        [ForeignKey("RatedEntityId")]
        public virtual BlogComment RatedEntity { get; set; }
    }
}