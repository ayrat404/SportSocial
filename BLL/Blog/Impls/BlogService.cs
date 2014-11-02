using System.Collections.Generic;
using System.Threading;
using System.Web;
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

        public ServiceResult CreatePost(PostModel postModel)
        {
            Mapper.CreateMap<PostModel, Post>();
            var post = Mapper.Map<PostModel, Post>(postModel);
            post.Status = BlogPostStatus.New;
            post.RubricId = postModel.Rubric;
            post.UserId = HttpContext.Current.User.Identity.GetUserId();
            post.Lang = Thread.CurrentThread.CurrentCulture.Name;
            post.ImageUrl = postModel.Images[0].Url;//TODO проверять на наличие изображения в базе
            _repository.Add(post);
            _repository.SaveChanges();
            return new ServiceResult() {Success = true};
        }

        public IEnumerable<Rubric> GetRubrics()
        {
            return _repository.GetAll<Rubric>();
        }
    }
}