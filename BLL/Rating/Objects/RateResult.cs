using BLL.Common.Objects;

namespace BLL.Rating.Objects
{
    public class RateResult : ServiceResult
    {
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
    }
}