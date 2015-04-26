using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BLL.Common.Objects;

namespace BLL.Blog.ViewModels.Base
{
    public class PostBaseModel
    {
        public PostBaseModel()
        {
            Rubric = 1;
        }
        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Text { get; set; }
        
        [Required]
        public int Rubric { get; set; }

        public Images[] Images { get; set; }
        
        public string VideoUrl { get; set; }

        public bool IsVideo { get; set; }
    }
}