using System.ComponentModel.DataAnnotations;
using Knoema.Localization;

namespace BLL.Login.ViewModels
{
    [Localized]
    public class ConfirmSmsCode
    {
        [Required(ErrorMessage = "Необходимо ввести код")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Код должен содержать 4 символа")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Необходимо ввести подтверждение пароля")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        //[Required]
        public string Phone { get; set; }
    }
}