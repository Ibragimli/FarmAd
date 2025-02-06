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

namespace FarmAd.Persistence.Contexts
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<SubCategory>()
            //    .HasKey(x => new { x.CategoryId });

            //builder.Entity<Poster>()
            //   .HasKey(x => new { x.PosterFeaturesId });

            //builder.Entity<PosterUserId>()
            //    .HasKey(x => new { x.PosterId, x.AppUserId });

            //builder.Entity<PosterUserId>()
            //    .HasOne(x => x.AppUser)
            //    .WithMany(x => x.PosterUserIds)
            //    .HasForeignKey(x => x.AppUserId);

            //builder.Entity<PosterUserId>()
            //           .HasOne(x => x.Poster)
            //           .WithMany(x => x.PosterUserIds)
            //           .HasForeignKey(x => x.PosterId);

            //builder.Entity<WishItem>()
            //    .HasKey(x => new { x.AppUserId, x.PosterId });

            //builder.Entity<WishItem>()
            //         .HasOne(x => x.AppUser)
            //         .WithMany(x => x.WishItems)
            //         .HasForeignKey(x => x.AppUserId);

            //builder.Entity<WishItem>()
            //           .HasOne(x => x.Poster)
            //           .WithMany(x => x.WishItems)
            //           .HasForeignKey(x => x.PosterId);


            //builder.Entity<PosterImage>()
            //    .HasKey(x => new { x.PosterId });

            //builder.Entity<Payment>()
            //    .HasKey(x => new { x.AppUserId, x.PosterId });

            //builder.Entity<PosterFeatures>()
            //    .HasKey(x => new { x.CityId, x.SubCategoryId });


            builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
    }
}