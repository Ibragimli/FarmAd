using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Abstractions.Services.Configurations;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Repositories.Category;
using FarmAd.Application.Repositories.City;
using FarmAd.Application.Repositories.ContactUs;
using FarmAd.Application.Repositories.Endpoint;
using FarmAd.Application.Repositories.ImageSetting;
using FarmAd.Application.Repositories.Menu;
using FarmAd.Application.Repositories.Payment;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.ProductFeature;
using FarmAd.Application.Repositories.ProductImage;
using FarmAd.Application.Repositories.ServiceDuration;
using FarmAd.Application.Repositories.Setting;
using FarmAd.Application.Repositories.SubCategory;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Application.Repositories.UserTerm;
using FarmAd.Application.Repositories.WishItem;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service;
using FarmAd.Infrastructure.Service.User;
using FarmAd.Persistence.Contexts;
using FarmAd.Persistence.Repositories.Category;
using FarmAd.Persistence.Repositories.City;
using FarmAd.Persistence.Repositories.ContactUs;
using FarmAd.Persistence.Repositories.Endpoint;
using FarmAd.Persistence.Repositories.ImageSetting;
using FarmAd.Persistence.Repositories.Menu;
using FarmAd.Persistence.Repositories.Payment;
using FarmAd.Persistence.Repositories.Product;
using FarmAd.Persistence.Repositories.ProductFeature;
using FarmAd.Persistence.Repositories.ProductImage;
using FarmAd.Persistence.Repositories.ServiceDuration;
using FarmAd.Persistence.Repositories.Setting;
using FarmAd.Persistence.Repositories.SubCategory;
using FarmAd.Persistence.Repositories.UserAuthentication;
using FarmAd.Persistence.Repositories.UserTerm;
using FarmAd.Persistence.Repositories.WishItem;
using FarmAd.Persistence.Service.Area;
using FarmAd.Persistence.Service.User;
using FarmAd.Persistence.Services;
using FarmAd.Persistence.Services.Configurations;
using FarmAd.Persistence.Services.User;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {

            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductUserIdWriteRepository, ProductUserIdWriteRepository>();

            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IOTPService, OTPService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
            services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
            services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<ICityReadRepository, CityReadRepository>();
            services.AddScoped<ICityWriteRepository, CityWriteRepository>();
            services.AddScoped<IContactUsReadRepository, ContactUsReadRepository>();
            services.AddScoped<IContactUsWriteRepository, ContactUsWriteRepository>();
            services.AddScoped<IImageSettingReadRepository, ImageSettingReadRepository>();
            services.AddScoped<IImageSettingWriteRepository, ImageSettingWriteRepository>();
            services.AddScoped<IProductImageReadRepository, ProductImageReadRepository>();
            services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();
            services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
            services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();
            services.AddScoped<IProductFeatureReadRepository, ProductFeatureReadRepository>();
            services.AddScoped<IProductFeatureWriteRepository, ProductFeatureWriteRepository>();
            services.AddScoped<IServiceDurationReadRepository, ServiceDurationReadRepository>();
            services.AddScoped<IServiceDurationWriteRepository, ServiceDurationWriteRepository>();
            services.AddScoped<ISettingWriteRepository, SettingWriteRepository>();
            services.AddScoped<ISettingReadRepository, SettingReadRepository>();
            services.AddScoped<ISubCategoryReadRepository, SubCategoryReadRepository>();
            services.AddScoped<ISubCategoryWriteRepository, SubCategoryWriteRepository>();
            services.AddScoped<IUserAuthenticationReadRepository, UserAuthenticationReadRepository>();
            services.AddScoped<IUserAuthenticationWriteRepository, UserAuthenticationWriteRepository>();
            services.AddScoped<IUserTermReadRepository, UserTermReadRepository>();
            services.AddScoped<IUserTermWriteRepository, UserTermWriteRepository>();
            services.AddScoped<IWishItemReadRepository, WishItemReadRepository>();
            services.AddScoped<IWishItemWriteRepository, WishItemWriteRepository>();
            services.AddScoped<IProductCreateServices, ProductCreateServices>();
            services.AddScoped<IAdminCityServices, AdminCityServices>();
            services.AddScoped<IAdminCategoryServices, AdminCategoryServices>();




            // Identity Konfigürasyonu
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

        }
    }
}
