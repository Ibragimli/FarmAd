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
    public class ServiceDurationConfiguration : IEntityTypeConfiguration<ServiceDuration>
    {
        public void Configure(EntityTypeBuilder<ServiceDuration> builder)
        {
            builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");

        }
    }
}
