using System;

namespace DAL.DomainModel
{
    public class BlogImage: IEntity, IAuditable, IDeletable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}