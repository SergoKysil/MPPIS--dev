namespace Application.Dto
{
    public class AddStorageDataDto
    {
        public int? Id { get; set; }

        public decimal CountProduction { get; set; }

        public UserDto User { get; set; }
    }
}
