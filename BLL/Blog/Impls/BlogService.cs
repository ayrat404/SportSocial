using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;
using DAL.DomainModel;
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
            Mapper.CreateMap<CreatePostModel, Post>();
            var post = Mapper.Map<CreatePostModel, Post>(createPostModel);
            post.Status = BlogPostStatus.New;
            post.RubricId = createPostModel.Rubric;
            post.UserId = HttpContext.Current.User.Identity.GetUserId();
            post.Lang = Thread.CurrentThread.CurrentCulture.Name;
            post.ImageUrl = createPostModel.Images[0].Url;//TODO проверять на наличие изображения в базе
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
                .Select(p => new PostForAdminViewModel()
                {
                    Id = p.Id,
                    Status = p.Status,
                    Date = p.Created,
                    Title = p.Title,
                });
        }
    }
}