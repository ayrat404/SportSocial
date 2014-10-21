using System.ComponentModel.DataAnnotations;
using Knoema.Localization;

namespace SportSocial.Models
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