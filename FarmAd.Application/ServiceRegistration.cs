using FarmAd.Application.Features.Commands.User.LoginUser;
using FarmAd.Application.MappingProfiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(LoginUserCommandHandler).Assembly);
            services.AddAutoMapper(opt => { opt.AddProfile(new AppProfile()); });

        }
    }
}
