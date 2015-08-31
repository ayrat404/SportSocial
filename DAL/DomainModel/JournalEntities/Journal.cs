using System;
using System.Collections.Generic;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.JournalEntities
{
    public class Journal: IEntity, IAuditable, IDeletable, IHasComments<JournalComment>, IHasRating<JournalRating>
    {
        public int Id { get; set; }
        public int TotalRating { get; set; }
        public string Text { get; set; }

        public int UserId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }

        public AppUser User { get; set; }
        public ICollection<JournalComment> Comments { get; set; }
        public ICollection<JournalRating> RatingEntites { get; set; }
        public ICollection<JournalMedia> Media { get; set; }
        public ICollection<JournalTag> Tags { get; set; }
    }
}