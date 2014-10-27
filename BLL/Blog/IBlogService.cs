using System.Collections.Generic;
using BLL.Blog.ViewModels;
using DAL.DomainModel;

namespace BLL.Blog
{
    public interface IBlogService
    {
        void CreatePost(PostModel postModel);
        IEnumerable<Rubric> GetRubrics();
    }
}