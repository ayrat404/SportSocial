using BLL.Blog.ViewModels;
using BLL.Comments.Objects;
using BLL.Login.Impls;
using BLL.Rating;
using BLL.Social.Journals.Objects;
using DAL.DomainModel;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.ConferenceEntities;
using DAL.DomainModel.FeedBackEntities;
using DAL.DomainModel.Interfaces;
using DAL.DomainModel.JournalEntities;
using Profile = AutoMapper.Profile;

namespace BLL.Comments.MapProfiles
{
    public class CommentsProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<AppUser, AuthorVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName()))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar));

            CreateMap<CommentEntityBase, Comment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.ByFortress ? LoginService.DefaultFortressAvatar : src.User.Profile.Avatar))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created.ToString()))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ByFortress ? "Fortress" : src.User.Name))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

            CreateMap<BlogComment, Comment>()
                .IncludeBase<CommentEntityBase, Comment>()
                .ForMember(dest => dest.CommentFor, opt => opt.MapFrom(src => MapCommentFor(src)));

            CreateMap<FeedbackComment, Comment>()
                .IncludeBase<CommentEntityBase, Comment>()
                .ForMember(dest => dest.CommentFor, opt => opt.MapFrom(src => MapCommentFor(src)));

            CreateMap<ConferenceComment, Comment>()
                .IncludeBase<CommentEntityBase, Comment>()
                .ForMember(dest => dest.CommentFor, opt => opt.MapFrom(src => MapCommentFor(src)));

            ConfigureCommentSocial();
        }


        private void ConfigureCommentSocial()
        {
            //CreateMap<CommentEntityBase, CommentSocial>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User))
            //    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created.ToString()))
            //    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

            CreateMap<JournalComment, Comment>()
                .IncludeBase<CommentEntityBase, Comment>()
                .ForMember(dest => dest.CommentFor, opt => opt.MapFrom(src => MapCommentFor(src)))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => RatingMapper.MapRating(src)));

            CreateMap<AchievementComment, Comment>()
                .IncludeBase<CommentEntityBase, Comment>()
                .ForMember(dest => dest.CommentFor, opt => opt.MapFrom(src => MapCommentFor(src)))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => RatingMapper.MapRating(src)));
        }


        private CommentFor MapCommentFor<TComment, TEntity>(CommentEntity<TComment, TEntity> src) 
            where TComment : CommentEntityBase
            where TEntity: class
        {
            if (src.CommentForId.HasValue)
            {
                return new CommentFor
                {
                    Id = src.CommentForId.Value,
                    Name = src.CommentFor.ByFortress ? "Fortress" : src.CommentFor.User.Name,
                };
            }
            return null;
        }

    }
}