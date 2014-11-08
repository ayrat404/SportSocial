using System;

namespace DAL.DomainModel.Interfaces
{
    public interface IAuditable
    {
        DateTime Created { get; set; }

        DateTime Modified { get; set; }
    }
}