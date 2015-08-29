using System;
using System.Collections.Generic;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.JournalEntities
{
    public class Journal: IEntity, IAuditable, IDeletable, IHasComments<JournalComment>, IHasRating<JournalRating>
    {
        public int Id { get; set; }
        public int TotalRating { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }

        public ICollection<JournalComment> Comments { get; set; }
        public ICollection<JournalRating> RatingEntites { get; set; }
        public ICollection<JournalImage> Images { get; set; }
        public ICollection<JournalVideo> Videos { get; set; }
    }
}