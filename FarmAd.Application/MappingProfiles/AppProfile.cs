using AutoMapper;
using FarmAd.Application.DTOs.User;
using FarmAd.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmAd.Application.MappingProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ProductCreateDto, ProductFeature>();


        }
    }
}
