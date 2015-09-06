using System;
using System.Collections.Generic;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.ConferenceEntities;
using DAL.DomainModel.FeedBackEntities;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;

namespace BLL.Comments.Impls
{
    public class UniversalCommentService : ICommentService
    {
        private IRepository _repository;
        private ICurrentUser _currentUser;

        public UniversalCommentService(ICurrentUser currentUser, IRepository repository)
        {
            _currentUser = currentUser;
            _repository = repository;
        }

        public Comment AddComment(CreateCommentViewModel createCommentViewModel)
        {
            return CreateGenericService(createCommentViewModel.ItemType).AddComment(createCommentViewModel);
        }

        public IEnumerable<Comment> LoadComments(int itemId, CommentItemType itemType)
        {
            return CreateGenericService(itemType).LoadComments(itemId, itemType);
        }

        private ICommentService CreateGenericService(CommentItemType type)
        {
            Type ratingServiseType = typeof (CommentServiceWork<,>)
                .MakeGenericType(GetComentedEntityByType(type), GetCommentEntityByType(type));
            return (ICommentService) Activator.CreateInstance(ratingServiseType, _repository, _currentUser);
        }

        private Type GetComentedEntityByType(CommentItemType type)
        {
            switch (type)
            {
                case CommentItemType.Article:
                    return typeof (Post);
                case CommentItemType.Conference:
                    return typeof (Conference);
                case CommentItemType.Feedback:
                    return typeof (Feedback);
                case CommentItemType.Journal:
                    return typeof (Journal);
            }
            return null;
        }

        private Type GetCommentEntityByType(CommentItemType type)
        {
            switch (type)
            {
                case CommentItemType.Article:
                    return typeof (BlogComment);
                case CommentItemType.Conference:
                    return typeof(ConferenceComment);
                case CommentItemType.Feedback:
                    return typeof (FeedbackComment);
                case CommentItemType.Journal:
                    return typeof (JournalComment);
            }
            return null;
        }
    }
}