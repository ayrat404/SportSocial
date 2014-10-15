using System.ComponentModel.DataAnnotations;

namespace SportSocial.Models
{
    public class SignInModel
    {
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Pass { get; set; }
    }
}