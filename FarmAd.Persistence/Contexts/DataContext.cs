using FarmAd.Domain.Entities.Identity;
using FarmAd.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;

namespace FarmAd.Persistence.Contexts
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImageSetting> ImageSettings { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductUserId> ProductUserIds { get; set; }
        public DbSet<WishItem> WishItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<UserAuthentication> UserAuthentications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ServiceDuration> ServiceDurations { get; set; }
        public DbSet<UserTerm> UserTerms { get; set; }

        public DbSet<IdentityUserRole<string>> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.Entity<ProductUserId>()
            //    .HasOne(x => x.AppUser)
            //    .WithMany(x => x.ProductUserIds)
            //    .HasForeignKey(x => x.AppUserId);

            //builder.Entity<ProductUserId>()
            //    .HasOne(x => x.Product)
            //    .WithMany(x => x.ProductUserIds)
            //    .HasForeignKey(x => x.ProductId);


            //builder.Entity<WishItem>()
            //    .HasOne(x => x.AppUser)
            //    .WithMany(x => x.WishItems)
            //    .HasForeignKey(x => x.AppUserId);

            //builder.Entity<WishItem>()
            //    .HasOne(x => x.Product)
            //    .WithMany(x => x.WishItems)
            //    .HasForeignKey(x => x.ProductId);

            builder.Entity<ProductImage>()
                .HasKey(x => new { x.Id });

            //builder.Entity<ProductImage>()
            //  .HasOne(x => x.Product)
            //  .WithMany(x => x.ProductImages)
            //  .HasForeignKey(x => x.ProductId);


            builder.Entity<Payment>()
                .HasKey(x => new { x.AppUserId, x.ProductId });

            //builder.Entity<Payment>()
            //    .HasOne(x => x.Product)
            //    .WithMany(x => x.Payments)
            //    .HasForeignKey(x => x.ProductId);

            //builder.Entity<Payment>()
            //.HasOne(x => x.AppUser)
            //.WithMany(x => x.Payments)
            //.HasForeignKey(x => x.AppUserId);

            builder.Entity<ProductFeature>().HasKey(x => x.Id);
            //builder.Entity<ProductFeature>()
            //    .HasOne(x => x.SubCategory)
            //    .WithMany() 
            //    .HasForeignKey(x => x.SubCategoryId);

            //builder.Entity<ProductFeature>()
            //    .HasOne(x => x.City)
            //    .WithMany(x => x.ProductFeatures) Aynı şekilde, .WithMany(y => y.ProductFeatures) kullanılabilir.
            //    .HasForeignKey(x => x.CityId);


            // Apply all configurations automatically from the assembly
            builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);


            builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
    }
}