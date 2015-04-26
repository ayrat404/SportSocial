using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.MapProfiles
{
    public class PostPreviewProfile : Profile
    {
        protected override void Configure()
        {
            //var currentUser = DependencyResolver.Current.GetService<ICurrentUser>();
            CreateMap<Post, PostPreviewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ItemInfo.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ItemInfo.AuthorName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created.ToShortDateString()))
                .ForMember(dest => dest.RatingInfo.Rating, opt => opt.MapFrom(src => src.TotalRating))
                .ForMember(dest => dest.Text, 
                           opt => opt.MapFrom(src => Regex.Replace(src.Text, @"<[^>]+>|&nbsp;", "").Trim()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ItemInfo.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.RatingInfo.IsLiked, 
                           opt => opt.
                               MapFrom(src => src.RatingEntites
                                   .Any(r => r.UserId == DependencyResolver.Current.GetService<ICurrentUser>().UserId
                                             && r.RatingType == RatingType.Like)))
                .ForMember(dest => dest.RatingInfo.IsDisiked, 
                           opt => opt.
                               MapFrom(src => src.RatingEntites.
                                   Any(r => r.UserId == DependencyResolver.Current.GetService<ICurrentUser>().UserId
                                            && r.RatingType == RatingType.Dislike)));
        }
    }
}