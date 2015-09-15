using System.Collections.Generic;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;

namespace BLL.Comments.Impls
{
    internal class CommentServiceWork<TEntity, TCommentEntity>: ICommentService
        where TEntity : class, IHasComments<TCommentEntity>
        where TCommentEntity : CommentEntityBase
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public CommentServiceWork(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public Comment AddComment(CreateCommentViewModel createCommentViewModel)
        {
            var entity = _repository.Find<TEntity>(createCommentViewModel.EntityId);
            if (entity != null)
            {
                var comment = _repository.Create<TCommentEntity>();
                comment.CommentForId = createCommentViewModel.CommentForId;
                comment.CommentedEntityId = createCommentViewModel.EntityId;
                comment.UserId = _currentUser.UserId;
                comment.Text = createCommentViewModel.Text;
                comment.ByFortress = _currentUser.IsAdmin ? createCommentViewModel.ByFortress : false;
                _repository.Add(comment);
                _repository.SaveChanges();
                
                var resultComment = comment.MapTo<Comment>();
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