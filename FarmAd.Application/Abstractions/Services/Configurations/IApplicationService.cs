﻿using FarmAd.Application.DTOs.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Configurations
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinitionEndpoint(Type type);
    }
}
