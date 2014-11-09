using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using AutoMapper;
using BLL.Blog.Enums;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;
using BLL.Common.Services.Rating;
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
        private readonly IRatingService _ratingService;

        public BlogService(IRepository repository, IRatingService ratingService)
        {
            _repository = repository;
            _ratingService = ratingService;
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

        public ServiceResult RaitBlog(BlogRatingViewModel model)
        {
            return _ratingService.Rate<Post, PostRating>(model.Id, model.ActionType);
        }

        public void AddComment(CreateCommentViewModel createCommentViewModelModel)
        {
            var blogComment = createCommentViewModelModel.MapTo<BlogComment>();
            blogComment.UserId = HttpContext.Current.User.Identity.GetUserId();
            _repository.Add(blogComment);
            _repository.SaveChanges();
        }

        public IEnumerable<Comment> LoadComments(int postId)
        {
            return _repository
                .Queryable<BlogComment>()
                .Where(c => c.PostId == postId && !c.Deleted)
                .AsNoTracking()
                .MapEachTo<Comment>();
        }

        public BlogPostViewModel GetPost(int id)
        {
            var post =  _repository.Queryable<Post>()
                .Where(p => p.Id == id && !p.Deleted)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .AsNoTracking()
                .Single()
                .MapTo<BlogPostViewModel>();
            return post;
        }
    }
}