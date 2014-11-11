using System.Collections.Generic;

namespace DAL.DomainModel.Interfaces
{
    public interface IHasComments<T>: IEntity where T: class
    {
        ICollection<T> Comments { get; set; }
    }
}