using System.ComponentModel.DataAnnotations;

namespace MPPIS.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Введіть коректний email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
