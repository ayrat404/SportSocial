using System.ComponentModel.DataAnnotations;

namespace BLL.Login.ViewModels
{
    public class ChangePhoneModel
    {
        [Required(ErrorMessage = "Необходимо ввести номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Необходимо ввести код подтверждения")]
        public string Code { get; set; }
    }
}