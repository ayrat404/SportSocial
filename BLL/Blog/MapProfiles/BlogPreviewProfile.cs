using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using BLL.Blog.ViewModels;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.MapProfiles
{
    public class BlogPreviewProfile : Profile
    {
        protected override void Configure()
        {
            //var currentUser = DependencyResolver.Current.GetService<ICurrentUser>();
            CreateMap<Post, PostPreviewViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created.ToShortDateString()))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.TotalRating))
                .ForMember(dest => dest.Text, 
                           opt => opt.MapFrom(src => Regex.Replace(src.Text, @"<[^>]+>|&nbsp;", "").Trim()))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.IsVideo, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.VideoUrl)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.Images,
                    opt => opt
                        .MapFrom(src => new Images {Id = 1, Url = src.ImageUrl}))
                .ForMember(dest => dest.IsLiked, 
                           opt => opt.
                               MapFrom(src => src.RatingEntites
                                   .Any(r => r.UserId == DependencyResolver.Current.GetService<ICurrentUser>().UserId
                                             && r.RatingType == RatingType.Like)))
                .ForMember(dest => dest.IsDisiked, 
                           opt => opt.
                               MapFrom(src => src.RatingEntites.
                                   Any(r => r.UserId == DependencyResolver.Current.GetService<ICurrentUser>().UserId
                                            && r.RatingType == RatingType.Dislike)));
        }
    }
}