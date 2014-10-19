using System.ComponentModel.DataAnnotations;

namespace SportSocial.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Не введен номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не введен пароль")]
        public string Pass { get; set; }
    }
}