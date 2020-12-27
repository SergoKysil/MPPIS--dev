using Domain.RDBMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.RDBMS.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(50);

        }
    }
}
