using System.ComponentModel.DataAnnotations;
using Knoema.Localization;

namespace BLL.Login.ViewModels
{
    [Localized]
    public class RegistratioinModel
    {
        [Required(ErrorMessage = "Не введен номер телефона")]
        [RegularExpression(@"^\d$", ErrorMessage = "Номер телефона должен содержать только цифры без сивола \"+\".")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не введено имя")]
        public string Name { get; set; }
    }
}