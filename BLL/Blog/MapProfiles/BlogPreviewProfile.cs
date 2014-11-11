using System.Linq;
using AutoMapper;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;
using DAL.DomainModel.BlogEntities;

namespace BLL.Blog.MapProfiles
{
    public class BlogPreviewProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Post, PostPreviewViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.TotalRating))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.Images,
                    opt => opt
                        .MapFrom(src => new Images {Id = 1, Url = src.ImageUrl}));
        }
    }
}