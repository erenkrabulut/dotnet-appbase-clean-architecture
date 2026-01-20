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
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogins");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProviderKey)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ProviderValue)
                .HasMaxLength(200);
        }
    }
}
