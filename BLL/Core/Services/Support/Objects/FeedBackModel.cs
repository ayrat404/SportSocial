using System.ComponentModel.DataAnnotations;

namespace BLL.Core.Services.Support.Objects
{
    public class FeedBackModel
    {
        [EmailAddress(ErrorMessage = "Неправильное написание электронной почты")]
        [Required(ErrorMessage = "Укажите электронную почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите текст")]
        public string Text { get; set; }
    }
}