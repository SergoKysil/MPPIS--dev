using System;
using System.Collections.Generic;

namespace Domain.RDBMS.Entities
{
    public class User : IEntityBase
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime RegisteredDate { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsDeleted { get; set; }

        public int RoleId { get; set; }

        public int LocationId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Location Location { get; set; }

        public virtual List<Product> Products { get; set; }

        public virtual List<TokenRefresh> Tokens { get; set; }



    }
}
