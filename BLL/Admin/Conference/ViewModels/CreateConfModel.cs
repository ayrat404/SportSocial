using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Admin.Conference.ViewModels
{
    public class CreateConfModel
    {
        [Required]
        public string Title { get; set; }

        //[Required]
        public string Url { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Description { get; set; }
    }
}