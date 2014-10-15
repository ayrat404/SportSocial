namespace SportSocial.Models
{
    public class ConfirmSmsCode
    {
        public string Code { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Phone { get; set; }
    }
}