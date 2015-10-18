using System;
using System.Collections.Generic;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel
{
    public class SmsCode: IEntity, IAuditable, IDeletable
    {
        public SmsCode()
        {
            Smses = new List<SmsMessage>();
        }
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Code { get; set; }

        public DateTime RetryTime { get; set; }

        public DateTime Expired { get; set; }

        public bool Deleted { get; set; }

        public bool Verified { get; set; }

        public virtual AppUser User { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public ICollection<SmsMessage> Smses { get; set; }
    }
}