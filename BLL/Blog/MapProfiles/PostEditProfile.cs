using AutoMapper;
using BLL.Blog.ViewModels;
using BLL.Blog.ViewModels.Base;
using BLL.Common.Objects;
using DAL.DomainModel.BlogEntities;

namespace BLL.Blog.MapProfiles
{
    public class PostEditProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Post, PostEditModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}