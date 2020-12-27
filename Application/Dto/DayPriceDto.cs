using System;
using System.Collections.Generic;

namespace Application.Dto
{
    public class DayPriceDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public List<StorageDataDto> StorageData { get; set; }
    }
}
