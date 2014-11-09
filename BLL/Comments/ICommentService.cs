using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using BLL.Infrastructure.Map;
using DAL.DomainModel;
using DAL.DomainModel.BlogEntities;
using DAL.Repository.Interfaces;

namespace BLL.Comments
{
    public interface ICommentService
    {
        Comment AddComment(CreateCommentViewModel createCommentViewModel);
        IEnumerable<Comment> LoadComments(int itemId, CommentItemType itemType);
    }

    class CommentService : ICommentService
    {
        private readonly IRepository _repository;

        public CommentService(IRepository repository)
        {
            _repository = repository;
        }

        public Comment AddComment(CreateCommentViewModel createCommentViewModel)
        {
            //switch (createCommentViewModel.ItemType)
            //{
            //    case CommentItemType.Article:
            //        var blogComment = createCommentViewModel.MapTo<BlogComment>();
            //        _repository.Add(blogComment);
            //        _repository.SaveChanges();
            //        return blogComment.MapTo<Comment>();
            //}
            //var commentedEntity = Activator.CreateInstance(GetComentedEntityByType(createCommentViewModel.ItemType));
            //commentedEntity = createCommentViewModel.MapTo(commentedEntity);
            //_repository.Add(commentedEntity);
            //_repository.SaveChanges();
            //return new ServiceResult {Success = true};
            return null;
        }

        public IEnumerable<Comment> LoadComments(int itemId, CommentItemType itemType)
        {
            switch (itemType)
            {
                case CommentItemType.Article:
                    var comments =  _repository
                        .Queryable<BlogComment>()
                        .Where(c => c.CommentedEntityId == itemId && !c.Deleted)
                        .AsNoTracking()
                        .MapEachTo<Comment>();
                    return comments;
                case CommentItemType.Conference:
                    //var  =  _repository
                    //    .Queryable<BlogComment>()
                    //    .Where(c => c.CommentedEntityId == itemId && !c.Deleted)
                    //    .AsNoTracking()
                    //    .MapEachTo<Comment>();
                    return null;
            }
            return null;
        }

        private Type GetComentedEntityByType(CommentItemType type)
        {
            switch (type)
            {
                case CommentItemType.Article:
                    return typeof (Post);
                case CommentItemType.Conference:
                    return typeof (Conference);
            }
            return null;
        }
    }
}