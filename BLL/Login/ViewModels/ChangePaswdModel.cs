using System.ComponentModel.DataAnnotations;

namespace BLL.Login.ViewModels
{
    public class ChangePaswdModel
    {
        [Required(ErrorMessage = "Необходимо ввести старый пароль")]
        public string Old { get; set; }

        [Required(ErrorMessage = "Необходимо ввести новый пароль")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        public string New { get; set; }

        [Required(ErrorMessage = "Необходимо ввести подтверждение пароля")]
#pragma warning disable 618
        [System.Web.Mvc.Compare("New", ErrorMessage = "Пароли не совпадают")]
#pragma warning restore 618
        public string ConfirmNew { get; set; }
    }
}