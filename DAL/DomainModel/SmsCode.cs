using System;

namespace DAL.DomainModel
{
    public class SmsCode: AuditableEntity, IDeletable
    {
        public string UserId { get; set; }

        public string Code { get; set; }

        public DateTime RetryDate { get; set; }

        public DateTime Expired { get; set; }

        public bool Deleted { get; set; }

        public virtual AppUser User { get; set; }
    }
}