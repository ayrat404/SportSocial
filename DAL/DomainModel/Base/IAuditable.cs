using System;

namespace DAL.DomainModel
{
    public interface IAuditable
    {
        DateTime Created { get; set; }

        DateTime Modified { get; set; }
        
        bool Deleted { get; set; }
    }
}