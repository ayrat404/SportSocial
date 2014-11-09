﻿using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.BlogEntities
{
    public class PostRating: IEntity, IRatingEntity<Post>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int RatedEntityId { get; set; }

        public RatingType RatingType { get; set; }

        [ForeignKey("RatedEntityId")]
        public virtual Post RatedEntity { get; set; }
        public virtual AppUser User { get; set; }
    }
}