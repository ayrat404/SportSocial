using System.ComponentModel.DataAnnotations;
using BLL.Blog.ViewModels.Base;

namespace BLL.Blog.ViewModels
{
    public class PostEditModel: PostCreateModel
    {
        [Required]
        public int Id { get; set; }
    }
}