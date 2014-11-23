using System;
using BLL.Common.Objects;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.ViewModels
{
    public class PostForAdminViewModel: HasDate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public BlogPostStatus Status { get; set; }
        public string Url { get; set; }
    }
}