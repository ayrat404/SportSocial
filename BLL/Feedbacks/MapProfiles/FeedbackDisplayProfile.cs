using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL.Common.Services.CurrentUser;
using BLL.Feedbacks.Objects;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.FeedBackEntities;

namespace BLL.Feedbacks.MapProfiles
{
    public class FeedbackDisplayProfile: Profile
    {

        protected override void Configure()
        {
            CreateMap<CreateFeedbackModel, Feedback>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.FeedbackTypeId, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => DependencyResolver.Current.GetService<ICurrentUser>().UserId));

            CreateMap<Feedback, FeedbackPreviewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.FeedbackTypeId))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.FeedbackType.Label))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.FeedbackType.Label))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created.ToShortDateString()))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.TotalRating))

                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.AuthorAvatar, opt => opt.MapFrom(src => src.User.Profile.Avatar))


                .ForMember(dest => dest.TotalCommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.MoreCommentsCount,
                    opt => opt.
                        MapFrom(src => src.Comments.Count <= 3 ? 0 : src.Comments.Count - 3))
                .ForMember(dest => dest.Comments,
                    opt => opt.MapFrom(src => src.Comments.Skip(src.Comments.Count - 3).ToList()))


                .ForMember(dest => dest.IsLiked,
                    opt => opt.
                        MapFrom(src => src.RatingEntites
                            .Any(r => r.UserId == DependencyResolver.Current.GetService<ICurrentUser>().UserId
                                      && r.RatingType == RatingType.Like)))
                .ForMember(dest => dest.IsDisiked,
                    opt => opt.
                        MapFrom(src => src.RatingEntites.
                            Any(r => r.UserId == DependencyResolver.Current.GetService<ICurrentUser>().UserId
                                     && r.RatingType == RatingType.Dislike)))
                .ForMember(dest => dest.LikesCount,
                    opt => opt.MapFrom(src => src.RatingEntites.Count(r => r.RatingType == RatingType.Like)))
                .ForMember(dest => dest.DislikesCount,
                    opt => opt.MapFrom(src => src.RatingEntites.Count(r => r.RatingType == RatingType.Dislike)));
        }
    }
}