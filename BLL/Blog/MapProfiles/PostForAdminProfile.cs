using BLL.Blog.ViewModels;
using DAL.DomainModel;
using DAL.DomainModel.BlogEntities;
using Profile = AutoMapper.Profile;

namespace BLL.Blog.MapProfiles
{
    public class PostForAdminProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Post, PostForAdminViewModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Created.ToString()));
        }
    }
}