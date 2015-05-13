using System.Collections.Generic;

namespace DAL.DomainModel.Interfaces
{
    public interface IHasRating<T>: IEntity where T: class
    {
        int TotalRating { get; set; }
        ICollection<T> RatingEntites { get; set; }
    }
}