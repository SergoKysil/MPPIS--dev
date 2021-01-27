using Domain.RDBMS.Entities;
using Infrastructure.RDBMS.Configuration;
using Infrastructure.RDBMS.Seeder;
using Microsoft.EntityFrameworkCore;
using MPPIS.Infrastructure.Configuration;



namespace Infrastructure
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Product> StorageData { get; set; }
        public DbSet<DayPrice> DayPrice { get; set; }
        public DbSet<RouteDay> RouteDay { get; set; }

        public DbSet<TokenRefresh> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new StorageDataConfiguration());
            modelBuilder.ApplyConfiguration(new DayPriceConfiguration());
            modelBuilder.ApplyConfiguration(new RouteDayConfiguration());
            modelBuilder.ApplyConfiguration(new TokenRefreshConfiguration());
           

            DataSeeder.Seed(modelBuilder);
        }

    }
}
