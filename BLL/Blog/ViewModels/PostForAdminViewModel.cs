using System;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog.ViewModels
{
    public class PostForAdminViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public BlogPostStatus Status { get; set; }
        public string Url { get; set; }
    }
}