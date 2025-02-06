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
    public class ContactUsConfiguration : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.Property(x => x.Fullname).HasMaxLength(30).IsRequired(true);
            builder.Property(x => x.Email).HasMaxLength(70).IsRequired(false);
            builder.Property(x => x.Subject).HasMaxLength(70).IsRequired(true);
            builder.Property(x => x.Message).HasMaxLength(2500).IsRequired(true);
            builder.Property(x => x.Phone).HasMaxLength(20).IsRequired(false);
        }
    }
}
