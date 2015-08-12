using BLL.Common.Objects;
using BLL.Rating.Models;

namespace BLL.Rating
{
    public interface IRatingService
    {
        ServiceResult Rate(RatingModel rateModel);
    }
}