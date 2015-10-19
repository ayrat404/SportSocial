using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.FeedBackEntities
{
    public class FeedbackRating: RatingEntity<Feedback>
    {
    }
}