using System.ComponentModel.DataAnnotations;

namespace MPPIS.Models
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage ="Введіть ім'я!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введіть по-батькові!")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Введіть прізвище!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Введіть коректний email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int LocationId { get; set; }

        [Required(ErrorMessage = "Ввеідть місто!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Ввеідть район!")]
        public string District { get; set; }

        [Required(ErrorMessage = "Ввеідть місто/село!")]
        public string Village { get; set; }

        [Required(ErrorMessage = "Ввеідть вулицю!")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Ввеідть номер будинку!")]
        public string HouseNumber { get; set; }

    }
}
