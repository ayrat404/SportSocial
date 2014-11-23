using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.MapProfiles
{
    public class BlogPostProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Post, BlogPostViewModel>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.TotalRating))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.Images,
                    opt => opt
                        .MapFrom(src => new[] {new Images {Id = 1, Url = src.ImageUrl}}))
                .ForMember(dest => dest.Comments,
                    opt => opt.MapFrom(src => src.Comments.Skip(src.Comments.Count - 3).ToList()))
                .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => CommentItemType.Article))
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