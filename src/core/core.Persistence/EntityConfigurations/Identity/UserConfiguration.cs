using core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.EntityConfigurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.RefreshTokens)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Logins)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}