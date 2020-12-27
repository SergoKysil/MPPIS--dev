namespace Domain.RDBMS.Entities
{
    public class Location : IEntityBase
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Village { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public virtual User User { get; set; }

    }
}
