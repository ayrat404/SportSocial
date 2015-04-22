using System.Collections.Generic;
using BLL.Blog.ViewModels.Base;
using DAL.DomainModel;

namespace BLL.Blog.ViewModels
{
    public class PostCreateModel: PostBaseModel
    {
        public IEnumerable<Rubric> Rubrics { get; set; }

        public bool IsFortressNews { get; set; }
    }
}