using AutoMapper;
using BLL.Comments.Objects;
using DAL.DomainModel.BlogEntities;

namespace BLL.Comments.MapProfiles
{
    public class CProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<BlogComment, Comment>()
                .ForMember(dest => dest.CommentFor,
                    opt => opt.MapFrom(src => src.CommentForId.HasValue
                        ? new CommentFor()
                        {
                            Id = src.CommentForId.Value,
                            Name = src.CommentFor.User.Name,
                        }
                        : null));
        }
    }
}