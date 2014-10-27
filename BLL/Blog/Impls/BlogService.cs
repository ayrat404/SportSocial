using System.Collections.Generic;
using BLL.Blog.ViewModels;
using DAL.DomainModel;
using DAL.Repository.Interfaces;

namespace BLL.Blog.Impls
{
    public class BlogService : IBlogService
    {
        private readonly IRepository _repository;

        public BlogService(IRepository repository)
        {
            _repository = repository;
        }

        public void CreatePost(PostModel postModel)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Rubric> GetRubrics()
        {
            return _repository.GetAll<Rubric>();
        }
    }
}