namespace Domain.RDBMS.Entities
{
    public class TokenRefresh : IEntityBase
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}
