using AutoMapper;
using BLL.Comments.Objects;
using BLL.Login.Impls;
using DAL.DomainModel.FeedBackEntities;
using DAL.DomainModel.Interfaces;

namespace BLL.Comments.MapProfiles
{
    public class FeedbackCommentsProfile: Profile
    {
        protected override void Configure()
        {
            //CreateMap<FeedbackComment, Comment>()
            //    .IncludeBase<CommentEntityBase, Comment>();
        }
    }
}