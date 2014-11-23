using System.Collections.Generic;

namespace DAL.DomainModel.Interfaces
{
    public interface IHasRating<T> where T: class
    {
        int TotalRating { get; set; }
        ICollection<T> RatingEntites { get; set; }
    }
}