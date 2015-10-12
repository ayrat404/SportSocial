using System;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel
{
    public class SmsMessage: IEntity
    {
        public SmsMessage()
        {
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        public int Id { get; set; }

        public int SmsCodeId { get; set; }

        public string Message { get; set; }

        public string Phone { get; set; }
        
        public int SmsProvider { get; set; }

        public string ExternalId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public SmsCode SmsCode { get; set; }
    }
}