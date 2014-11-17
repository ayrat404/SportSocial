using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using DAL.DomainModel;

namespace BLL.Blog.ViewModels
{
    public class CreatePostModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Text { get; set; }

        ////[Required]
        public Images[] Images { get; set; }

        [Required]
        public int Rubric { get; set; }

        public IEnumerable<Rubric> Rubrics { get; set; }
    }

    public class EditPostModel: CreatePostModel
    {
        public int Id { get; set; }
    }


    public class Images
    {
        public int Id { get; set; }

        public string Url { get; set; }
    }
}