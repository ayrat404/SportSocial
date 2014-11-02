using System.Collections.Generic;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;
using DAL.DomainModel;

namespace BLL.Blog
{
    public interface IBlogService
    {
        ServiceResult CreatePost(PostModel postModel);
        IEnumerable<Rubric> GetRubrics();
    }
}