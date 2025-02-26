using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Configurations;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Abstractions.Storage;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Enums;
using FarmAd.Infrastructure.Service;
using FarmAd.Infrastructure.Service.Storage.Azure;
using FarmAd.Infrastructure.Service.Storage.Local;
using FarmAd.Infrastructure.Service.Storage;
using FarmAd.Infrastructure.Service.Tokens;
using FarmAd.Infrastructure.Service.User;
using FarmAd.Persistence.Service;
using FarmAd.Persistence.Service.User;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Abstractions.Helpers;

namespace FarmAd.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IImageManagerService, ImageManagerService>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IImageManagerService, ImageManagerService>();
            services.AddScoped<ISmsSenderServices, SmsSenderServices>();
            services.AddScoped<IApplicationService, ApplicationService>(); // Eğer eksikse
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IRedisCacheServices, RedisCacheServices>();


        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }

        //elave olaraq 
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;

            }
        }
    }
}
