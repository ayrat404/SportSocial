using System;
using System.Collections.Generic;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.ConferenceEntities
{
    public class Conference: IEntity, IDeletable, IAuditable, IHasComments<ConferenceComment>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public DateTime Date { get; set; }

        public ConfStatus Status { get; set; }

        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual AppUser User { get; set; }
        public virtual ICollection<ConferenceComment> Comments { get; set; }
    }
}