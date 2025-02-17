using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Configurations;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service;
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

namespace FarmAd.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IImageManagerService, ImageManagerService>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IImageManagerService, ImageManagerService>();
            services.AddScoped<ISmsSenderServices, SmsSenderServices>();
            services.AddScoped<IApplicationService, ApplicationService>(); // Eğer eksikse

        }
    }
}
