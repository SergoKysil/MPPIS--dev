using System;

namespace Domain.RDBMS.Entities
{
    public class Product : IEntityBase
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public decimal CountProduction { get; set; }

        public int UserId { get; set; }

        public int DayPriceId { get; set; }

        public virtual User User { get; set; }

        public virtual DayPrice DayPrice { get; set; }
    }
}
