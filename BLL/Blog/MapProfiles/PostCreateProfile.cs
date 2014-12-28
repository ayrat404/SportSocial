using System.Threading;
using AutoMapper;
using BLL.Blog.ViewModels;
using DAL.DomainModel.BlogEntities;

namespace BLL.Blog.MapProfiles
{
    public class PostCreateProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<PostCreateModel, Post>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.RubricId, opt => opt.MapFrom(src => src.Rubric))
                .ForMember(dest => dest.Rubric, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Lang, opt => opt.MapFrom(src => Thread.CurrentThread.CurrentCulture.Name));
        }
    }
}