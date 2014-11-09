using System.Web.Mvc;
using AutoMapper;
using BLL.Comments.Objects;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel.BlogEntities;

namespace BLL.Comments.MapProfiles
{
    public class CreateCommentProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<CreateCommentViewModel, BlogComment>()
                .ForMember(dest => dest.CommentedEntityId, opts => opts.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.CommentForId, opts => opts.MapFrom(src => src.CommentForId))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.Text))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => 
                    (DependencyResolver.Current.GetService(typeof(ICurrentUser)) as ICurrentUser).UserId));
        }
    }
}