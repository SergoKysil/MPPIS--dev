using Domain.RDBMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.RDBMS.Configuration
{
    public class StorageDataConfiguration : IEntityTypeConfiguration<StorageData>
    {
        public void Configure(EntityTypeBuilder<StorageData> builder)
        {
            builder.ToTable("StorageData");

            builder.Property(p => p.Id)
                .IsRequired()
                .HasColumnName("id");

            builder.Property(p => p.DateAdded)
                .IsRequired()
                .HasColumnName("date_added")
                .HasColumnType("date")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.CountProduction)
                .IsRequired()
                .HasColumnName("count_production")
                .HasColumnType("decimal");

            builder.Property(p => p.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.User)
                .WithMany(p => p.StorageData)
                .HasForeignKey(k => k.UserId);

            builder.Property(p => p.DayPriceId).HasColumnName("day_price_id");

            builder.HasOne(d => d.DayPrice)
                .WithMany(p => p.StorageData)
                .HasForeignKey(k => k.DayPriceId);

        }
    }
}
