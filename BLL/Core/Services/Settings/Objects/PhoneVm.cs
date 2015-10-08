using System.ComponentModel.DataAnnotations;

namespace BLL.Core.Services.Settings.Objects
{
    public class PhoneVm
    {
        [Required(ErrorMessage = "Необходимо ввести номер телефона")]
        [RegularExpression(@"^[0-9]{11,13}$", ErrorMessage = "Номер телефона должен содержать только цифры в формате <код страны><номер> без сивола \"+\".")]
        public string Phone { get; set; }
    }
}