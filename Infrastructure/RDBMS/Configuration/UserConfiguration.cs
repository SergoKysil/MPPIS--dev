using Domain.RDBMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MPPIS.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(p => p.Id)
                .IsRequired()
                .HasColumnName("id");

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnName("firstname")
                .HasMaxLength(30);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnName("lastname")
                .HasMaxLength(30);

            builder.Property(p => p.MiddleName)
                .IsRequired()
                .HasColumnName("middlename")
                .HasMaxLength(30);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(70);

            builder.Property(p => p.PasswordHash)
                .IsRequired()
                .HasColumnName("passwordhash")
                .HasMaxLength(255);

            builder.Property(p => p.RegisteredDate)
                .HasColumnName("registered_date")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.IsEmailConfirmed)
                .IsRequired()
                .HasColumnName("is_email_confirmed")
                .HasDefaultValue(0)
                .HasColumnType("bit");

            builder.Property(p => p.RoleId).HasColumnName("role_id").HasDefaultValue(1);

            builder.Property(p => p.LocationId).HasColumnName("location_id");

            builder.HasOne(d=>d.Role)
                .WithMany(p=>p.User)
                .HasForeignKey(k=>k.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Location)
                .WithOne(p => p.User)
                .HasForeignKey<User>(k => k.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(d => d.StorageData)
                .WithOne(p => p.User)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);






        }
    }
}
