using System;
using System.ComponentModel.DataAnnotations;
using DAL.DomainModel.EnumProperties;
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
        public string PasswordRepeat { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не введено имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не введена фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не введена дата рождения")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Не введен пол")]
        public Sex Gender { get; set; }

        [Required(ErrorMessage = "Не выбран стаж")]
        public SportExperience SportTime { get; set; }
    }
}