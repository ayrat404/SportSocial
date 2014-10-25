using System.ComponentModel.DataAnnotations;
using Knoema.Localization;

namespace BLL.Login.ViewModels
{
    [Localized]
    public class SignInModel
    {
        [Required(ErrorMessage = "Не введен номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не введен пароль")]
        public string Pass { get; set; }
    }
}