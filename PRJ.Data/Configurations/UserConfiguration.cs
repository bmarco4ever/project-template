using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRJ.Domain.Entities;

namespace PRJ.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            /* Table */
            builder
                .ToTable("user")
                .HasKey(u => u.Id);
            /* Index */
            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            /* Fields */
            builder.Property(u => u.Id);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(60);
            builder.Property(u => u.Email);
            builder.Property(u => u.Password);
            builder.Property(u => u.CreatedBy);
            builder.Property(u => u.CreatedAt);
            builder.Property(u => u.ModifiedBy);
            builder.Property(u => u.ModifiedAt);
            builder.Property(u => u.IsBlocked);

            /* Relationships */
        }
    }
}
