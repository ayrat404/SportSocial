using System.ComponentModel.DataAnnotations;

namespace BLL.Blog.ViewModels
{
    public class CreateComment
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int ItemId { get; set; }

        public int CommentForId { get; set; }
    }
}