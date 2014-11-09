using AutoMapper;
using BLL.Blog.ViewModels;
using DAL.DomainModel.BlogEntities;

namespace BLL.Blog.MapProfiles
{
    public class CreateCommentProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<CreateCommentViewModel, BlogComment>()
                .ForMember(dest => dest.PostId, opts => opts.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.CommentForId, opts => opts.MapFrom(src => src.CommentForId))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.Text));
        }
    }
}