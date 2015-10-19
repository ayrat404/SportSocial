using System.ComponentModel.DataAnnotations;
using BLL.Common.Objects;

namespace BLL.Comments.Objects
{
    public class CreateCommentViewModel
    {
        [Required]
        public string CommentType { get; set; }

        [Required]
        public string Text { get; set; }

        //[Required]
        public CommentItemType EntityType { get; set; }

        [Required]
        public int EntityId { get; set; }

        public int? CommentForId { get; set; }

        public bool ByFortress { get; set; }
    }

    public class CreateCommentFromBlog
    {
        [Required]
        public string CommentType { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public CommentItemType ItemType { get; set; }

        [Required]
        public int ItemId { get; set; }

        public bool ByFortress { get; set; }

        public int? CommentForId { get; set; }
    }
}