using Domain.RDBMS.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.RDBMS.Seeder
{
    public static class DataSeeder
    { 

        public static void Seed(ModelBuilder builder)
        {
            Seed(builder.Entity<User>());
            Seed(builder.Entity<Role>());
            Seed(builder.Entity<Location>());
        }




        private static void Seed(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "User"
                },
                new Role
                {
                    Id = 2,
                    Name = "Admin"
                }
            );
        }


        private static void Seed(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Tester",
                    MiddleName = "Test",
                    LastName = "Testerovich",
                    Email = "test@gmail.com",
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "test"),
                    RoleId = 1,
                    LocationId = 1
                },
                new User
                {
                    Id = 2,
                    FirstName = "Admin",
                    MiddleName = "Adminovski",
                    LastName = "Adminovich",
                    Email = "admin@gmail.com",
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "admin"),
                    RoleId = 2,
                    LocationId = 2
                    
                }
            );
        }

        private static void Seed(EntityTypeBuilder<Location> builder)
        {
            builder.HasData(
                new Location
                {
                    Id = 1,
                    City = "Lviv",
                    District = "Mostisky",
                    Village = "Tvirzha",
                    Street = "Sagaydachnogo",
                    HouseNumber = "53"
                },
                new Location
                {
                    Id = 2,
                    City = "Lviv",
                    District = "Mostisky",
                    Village = "Tvirzha",
                    Street = "Sagaydachnogo",
                    HouseNumber = "53"
                }
                );
        }

    }
}
