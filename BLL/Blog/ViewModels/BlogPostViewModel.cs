using System;

namespace BLL.Blog.ViewModels
{
    public class BlogPostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public Images[] Images { get; set; }

        public DateTime Date { get; set; }

        public int Rating { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public Comment[] Comments { get; set; }
    }
}