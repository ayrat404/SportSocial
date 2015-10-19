using System;
using System.ComponentModel.DataAnnotations;
using DAL.DomainModel.EnumProperties;
using Knoema.Localization;

namespace BLL.Login.ViewModels
{
    public class RegistrationConfirm : RegistrationBase
    {
        public int? ImgId { get; set; }

        //[Required(ErrorMessage = "Не введено имя")]
        public string Name { get; set; }
    }

    [Localized]
    public class ConfirmSmsCode: RegistrationConfirm
    {
        [Required]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Необходимо ввести код")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Необходимо ввести подтверждение пароля")]
#pragma warning disable 618
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Пароли не совпадают")]
#pragma warning restore 618
        public string ConfirmPassword { get; set; }
    }
}