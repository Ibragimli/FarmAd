using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities;

namespace FarmAd.Infrastructure.Configurations
{

    public class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Describe).HasMaxLength(3000).IsRequired(true);
            builder.Property(x => x.Email).HasMaxLength(70).IsRequired(true);
            builder.Property(x => x.PhoneNumber).HasMaxLength(15).IsRequired(true);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

        }
    }
}
