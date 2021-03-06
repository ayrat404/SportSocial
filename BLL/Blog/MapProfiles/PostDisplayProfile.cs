﻿using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.MapProfiles
{
    public class PostDisplayProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Post, PostDisplayModel>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.AuthorAvatar, opt => opt.MapFrom(src => src.User.Profile.Avatar))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created.ToString()))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.TotalRating))
                .ForMember(dest => dest.TotalCommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.RubricTitle, opt => opt.MapFrom(src => src.Rubric.Name))
                .ForMember(dest => dest.MoreCommentsCount, 
                           opt => opt.
                                   MapFrom(src => src.Comments.Count <= 3 ? 0 : src.Comments.Count - 3))
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