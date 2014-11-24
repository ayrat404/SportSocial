using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using DAL.DomainModel;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;
namespace BLL.Comments
{
    public interface ICommentService<TEntity, TCommentEntity>
        where TEntity : class, IHasComments<TCommentEntity>
        where TCommentEntity : class, ICommentEntity<TEntity>
    {
    }

    public interface ICommentServiceMethods
    {
        Comment AddComment(CreateCommentViewModel createCommentViewModel);
        IEnumerable<Comment> LoadComments(int itemId, CommentItemType itemType);
    }

    public class CommentService<TEntity, TCommentEntity> : ICommentServiceMethods, ICommentService<TEntity, TCommentEntity>
        where TEntity : class, IHasComments<TCommentEntity>
        where TCommentEntity : class, ICommentEntity<TEntity>
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public CommentService(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public Comment AddComment(CreateCommentViewModel createCommentViewModel)
        {
            var entity = _repository.Find<TEntity>(createCommentViewModel.ItemId);
            if (entity != null)
            {
                var comment = _repository.Create<TCommentEntity>();
                comment.CommentForId = createCommentViewModel.CommentForId;
                comment.CommentedEntityId = createCommentViewModel.ItemId;
                comment.UserId = _currentUser.UserId;
                comment.Text = createCommentViewModel.Text;
                _repository.Add(comment);
                _repository.SaveChanges();
                
                //var blogComment = _repository.Find<TCommentEntity>(comment.Id);
                var resultComment = comment.MapTo<Comment>();
                resultComment.Name = _currentUser.UserName;
                resultComment.Avatar = _currentUser.User.Profile.Avatar;
                if (createCommentViewModel.CommentForId.HasValue && resultComment.CommentFor == null)
                {
                    comment = _repository.Find<TCommentEntity>(createCommentViewModel.CommentForId);
                    resultComment.CommentFor = new CommentFor
                    {
                        Id = comment.Id,
                        Name = comment.User.Name,
                    };
                }
                return resultComment;
            }
            return null;
        }

        public IEnumerable<Comment> LoadComments(int itemId, CommentItemType itemType)
        {
            var entity = _repository.Find<TEntity>(itemId);
            if (entity != null)
            {
                return entity
                    .Comments
                    .MapEachTo<Comment>();
            }
            return null;
        }
    }
}