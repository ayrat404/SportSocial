using System;
using BLL.Comments.Objects;
using BLL.Common.Objects;

namespace BLL.Blog.ViewModels
{
    public class BlogPostViewModel: IHasCommentViewModel, IItemInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public Images[] Images { get; set; }

        public DateTime Date { get; set; }

        public int Rating { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public int CommentsCount { get; set; }

        public CommentItemType ItemType { get; set; }

        public Comment[] Comments { get; set; }
    }
}