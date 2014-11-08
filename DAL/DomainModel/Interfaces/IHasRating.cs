using System.Collections.Generic;

namespace DAL.DomainModel.Interfaces
{
    public interface IHasRating<T>
    {
        int TotalRating { get; set; }
        ICollection<IRatingEntity<T>> RatingEntites { get; set; }
    }
}