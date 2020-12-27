using System;

namespace Application.Dto
{
    public class StorageDataDto
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public decimal CountProduction { get; set; }

        public UserDto User { get; set; }

        public DayPriceDto DayPrice { get; set; }
    }
}
