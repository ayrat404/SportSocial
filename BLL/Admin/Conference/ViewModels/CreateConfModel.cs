using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BLL.Admin.Conference.ViewModels
{
    public class CreateConfModel
    {
        [Required]
        public string Title { get; set; }

        //[Required]
        public string Url { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }
    }
}