using AutoMapper;
using BLL.Blog.ViewModels;
using DAL.DomainModel.BlogEntities;

namespace BLL.Blog.MapProfiles
{
    public class CommentsProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<BlogComment, Comment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.User.Profile.Avatar))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.CommentFor,
                        opt => opt.MapFrom(src => src.CommentForId.HasValue ? new CommentFor()
                            {
                                Id = src.CommentForId.Value,
                                Name = src.CommentFor.User.Name,
                            }
                            : null))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));
        }
    }
}