﻿using AutoMapper;
using BLL.Blog.ViewModels;
using DAL.DomainModel.BlogEntities;

namespace BLL.Blog.MapProfiles
{
    public class EditPostProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Post, CreatePostModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Rubric, opt => opt.MapFrom(src => src.RubricId))
                .ForMember(dest => dest.IsVideo, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.VideoUrl)))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => new[] {new Images {Url = src.ImageUrl}}));
        }
    }
}