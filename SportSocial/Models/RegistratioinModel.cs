using System.ComponentModel.DataAnnotations;

namespace SportSocial.Models
{
    public class RegistratioinModel
    {
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordRepeat { get; set; }
    }
}