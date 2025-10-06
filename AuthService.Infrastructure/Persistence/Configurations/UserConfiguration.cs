using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Username)
                .IsRequired() 
                .HasMaxLength(100);

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.Firstname)
                .HasMaxLength(50);

            builder.Property(u => u.Middlename)
                .HasMaxLength(50);

            builder.Property(u => u.Lastname)
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
