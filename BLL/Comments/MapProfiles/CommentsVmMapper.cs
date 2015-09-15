using BLL.Comments.Objects;
using BLL.Infrastructure.Map;
using BLL.Rating;
using DAL.DomainModel.Interfaces;

namespace BLL.Comments.MapProfiles
{
    public class CommentsVmMapper : MapperBase
    {
        public static CommentsVm Map<T>(IHasComments<T> entity) where T : CommentEntityBase
        {
            return new CommentsVm
            {
                Count = entity.Comments.Count,
                List = entity.Comments.MapEachTo<Comment>()
            };
        }
    }
}