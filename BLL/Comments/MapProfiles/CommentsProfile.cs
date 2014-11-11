﻿using AutoMapper;
using BLL.Comments.Objects;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.Interfaces;

namespace BLL.Comments.MapProfiles
{
    public class CommentsProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<ICommentEntity<object>, Comment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.User.Profile.Avatar))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                //.ForMember(dest => dest.CommentFor,
                //        opt => opt.MapFrom(src => src.CommentForId.HasValue ? new CommentFor()
                //            {
                //                Id = src.CommentForId.Value,
                //                Name = src.CommentFor.User.Name,
                //            }
                //            : null))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));
        }
    }
}