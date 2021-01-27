using Domain.RDBMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.RDBMS.Configuration
{
    public class TokenRefreshConfiguration : IEntityTypeConfiguration<TokenRefresh>
    {
        public void Configure(EntityTypeBuilder<TokenRefresh> builder)
        {
            builder.ToTable("TokenRefresh");

            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Tokens)
                .HasForeignKey(prop => prop.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Token)
                .IsRequired();
        }
    }
}
