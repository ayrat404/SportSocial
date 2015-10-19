using System;
using System.ComponentModel.DataAnnotations;
using DAL.DomainModel.EnumProperties;
using Knoema.Localization;

namespace BLL.Login.ViewModels
{
    [Localized]
    public class RegistratioinModel: RegistrationBase
    {
        [Required(ErrorMessage = "Не введен номер телефона")]
        [RegularExpression(@"^[0-9]{11,13}$", ErrorMessage = "Номер телефона должен содержать только цифры в формате <код страны><номер> без сивола \"+\".")]
        public string Phone { get; set; }

        //[Required(ErrorMessage = "Не введено имя")]
        public string Name { get; set; }
    }

    [Localized]
    public class RegistrationBase
    {
        public RegistrationBase()
        {
            BirthDay = DateTime.Now;
        }

        //[Required(ErrorMessage = "Не введена фамилия")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Не введена дата рождения")]
        public DateTime BirthDay { get; set; }

        //[Required(ErrorMessage = "Не введен пол")]
        public Sex Gender { get; set; }

        //[Required(ErrorMessage = "Не выбран стаж")]
        public SportExperience SportTime { get; set; }
    }

    public class ExternalRegistrationModel : RegistrationConfirm
    {
        
    }
}