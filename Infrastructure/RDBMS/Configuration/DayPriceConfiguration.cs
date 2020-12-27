using Domain.RDBMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.RDBMS.Configuration
{
    public class DayPriceConfiguration : IEntityTypeConfiguration<DayPrice>
    {
        public void Configure(EntityTypeBuilder<DayPrice> builder)
        {
            builder.ToTable("DayPrice");
            
            builder.Property(p => p.Id)
                .IsRequired()
                .HasColumnName("id");

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnName("price")
                .HasColumnType("money")
                .HasDefaultValue(0);

            builder.Property(p => p.Date)
                .IsRequired()
                .HasColumnName("date")
                .HasColumnType("date");

            builder.HasMany(d => d.StorageData)
                .WithOne(p => p.DayPrice)
                .HasForeignKey(k => k.DayPriceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
