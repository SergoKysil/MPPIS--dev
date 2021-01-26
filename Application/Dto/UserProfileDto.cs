using System.Collections.Generic;

namespace Application.Dto
{
    public class UserProfileDto
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public LocationDto LocationDto { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
