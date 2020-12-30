using System;
using System.ComponentModel.DataAnnotations;

namespace MPPIS.Models
{
    public class AddDayPriceViewModel
    {
        [Required(ErrorMessage = "Введіть ціну")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Введіть дату")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
