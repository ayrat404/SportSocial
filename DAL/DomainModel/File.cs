using System;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel
{
    public class BlogImage: IEntity, IAuditable, IDeletable
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}