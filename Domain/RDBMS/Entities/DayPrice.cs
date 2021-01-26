using System;
using System.Collections.Generic;

namespace Domain.RDBMS.Entities
{
    public class DayPrice : IEntityBase
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public virtual List<Product> StorageData { get; set; }

    }
}
