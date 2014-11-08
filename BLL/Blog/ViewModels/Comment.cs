using System;

namespace BLL.Blog.ViewModels
{
    public class Comment
    {
        public int Id { get; set; }

        public string Avatar { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        public CommentFor CommentFor { get; set; }
    }
}