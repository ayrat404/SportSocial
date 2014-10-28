using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DAL.DomainModel;

namespace BLL.Blog.ViewModels
{
    public class PostModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Images Images { get; set; }

        [Required]
        public int Rubric { get; set; }

        public IEnumerable<Rubric> Rubrics { get; set; }
    }

    public class Images
    {
        public int Id { get; set; }

        public string Url { get; set; }
    }
}