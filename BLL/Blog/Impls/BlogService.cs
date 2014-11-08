using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using AutoMapper;
using BLL.Blog.Enums;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;
using BLL.Infrastructure.Map;
using DAL.DomainModel;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Blog.Impls
{
    public class BlogService : IBlogService
    {
        private readonly IRepository _repository;

        public BlogService(IRepository repository)
        {
            _repository = repository;
        }

        public ServiceResult CreatePost(CreatePostModel createPostModel)
        {
            Mapper.CreateMap<CreatePostModel, Post>().ForMember(o => o.Rubric, opt => opt.Ignore());
            var post = Mapper.Map<CreatePostModel, Post>(createPostModel);
            post.Status = BlogPostStatus.New;
            post.RubricId = createPostModel.Rubric;
            //post.UserId = HttpContext.Current.User.Identity.GetUserId();
            post.Lang = Thread.CurrentThread.CurrentCulture.Name;
            if (createPostModel.Images != null && _repository.Find<BlogImage>(createPostModel.Images[0].Id) != null)
            {
                post.ImageUrl = createPostModel.Images[0].Url;//TODO проверять на наличие изображения в базе
            }
            _repository.Add(post);
            _repository.SaveChanges();
            return new ServiceResult() {Success = true};
        }

        public IEnumerable<Rubric> GetRubrics()
        {
            return _repository.GetAll<Rubric>();
        }

        public void ChangeStatus(int id, int status)
        {
            var post = _repository.Find<Post>(id);
            post.Status = (BlogPostStatus) status;
            _repository.SaveChanges();
        }

        public IEnumerable<PostForAdminViewModel> GetPostsForAdmin(BlogPostStatus? status, string query)
        {
            return _repository
                .GetAll<Post>()
                .MapEachTo<PostForAdminViewModel>();
        }

        public ServiceResult Rait(BlogRatingViewModel model)
        {
            var result = new ServiceResult {Success = true};
            switch (model.EntityType)
            {
                case RatingEntityType.Article:
                    var post = _repository.Find<Post>(model.Id);
                    if (post == null)
                    {
                        result.Success = false;
                        return result;
                    }
                    if (model.ActionType == RatingType.Like)
                        post.Likes += 1;
                    else
                        post.DisLikes += 1;
                    _repository.SaveChanges();
                    return result;
                case RatingEntityType.ArticleComment:
                    //var comment = _repository.Find<Post>(model.Id);
                    //if (post == null)
                    //{
                    //    result.Success = false;
                    //    return result;
                    //}
                    //if (model.ActionType == RatingActionType.Like)
                    //    post.Likes += 1;
                    //else
                    //    post.DisLikes += 1;
                    break;
            }
            return result;
        }

        public void AddComment(CreateComment createCommentModel)
        {
            var blogComment = createCommentModel.MapTo<BlogComment>();
            blogComment.UserId = HttpContext.Current.User.Identity.GetUserId();

            throw new NotImplementedException();
        }
    }
}