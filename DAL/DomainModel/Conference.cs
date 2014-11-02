using System;
using DAL.DomainModel.Base;
using DAL.DomainModel.EnumProperties;

namespace DAL.DomainModel
{
    public class Conference: IEntity, IDeletable, IAuditable
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public DateTime Date { get; set; }

        public ConfStatus Status { get; set; }

        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual AppUser User { get; set; }
    }
}