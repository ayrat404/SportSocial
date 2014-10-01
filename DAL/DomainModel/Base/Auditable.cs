using System;

namespace DAL.DomainModel
{
    public abstract class AuditableEntity: Entity, IAuditable
    {
        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
        
        public bool Deleted { get; set; }
    }
}