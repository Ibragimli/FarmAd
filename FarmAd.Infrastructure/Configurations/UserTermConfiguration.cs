using FarmAd.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Infrastructure.Configurations
{
    public class UserTermConfiguration : IEntityTypeConfiguration<UserTerm>
    {
        public void Configure(EntityTypeBuilder<UserTerm> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(80).IsRequired(true);
            builder.Property(x => x.Text).HasMaxLength(15000).IsRequired(true);
        }
    }
}
