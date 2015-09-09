using System;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Rating.Enums;
using BLL.Rating.Models;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.FeedBackEntities;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;

namespace BLL.Rating.Impls
{
    public class UniversalRatingService: IRatingService
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public UniversalRatingService(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public ServiceResult Rate(RatingModel rateModel)
        {
            Type ratingServiseType = typeof (RatingServiceWork<,>)
                .MakeGenericType(GetRatedEntity(rateModel.EntityType), GetRatingEntity(rateModel.EntityType));
            var ratingService = (IRatingService) Activator.CreateInstance(ratingServiseType, _repository, _currentUser);
            return ratingService.Rate(rateModel);
        }

        private Type GetRatedEntity(RatingEntityType entityType)
        {
            switch (entityType)
            {
                case RatingEntityType.Article:
                    return typeof (Post);
                case RatingEntityType.ArticleComment:
                    return typeof (BlogComment);
                case RatingEntityType.Feedback:
                    return typeof (Feedback);
                case RatingEntityType.FeedbackComment:
                    return typeof (FeedbackComment);
                case RatingEntityType.Record:
                    return typeof (Journal);
                case RatingEntityType.RecordComment:
                    return typeof (JournalComment);
            }
            return null;
        }

        private Type GetRatingEntity(RatingEntityType entityType)
        {
            switch (entityType)
            {
                case RatingEntityType.Article:
                    return typeof (PostRating);
                case RatingEntityType.ArticleComment:
                    return typeof (BlogCommentRating);
                case RatingEntityType.Feedback:
                    return typeof (FeedbackRating);
                case RatingEntityType.FeedbackComment:
                    return typeof (FeedbackCommentRating);
                case RatingEntityType.Record:
                    return typeof (JournalRating);
                case RatingEntityType.RecordComment:
                    return typeof (JournalCommentRating);
            }
            return null;
        }
    }
}