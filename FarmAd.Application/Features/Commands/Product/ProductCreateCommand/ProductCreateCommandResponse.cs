﻿using FarmAd.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductCreateCommand
{
    public class ProductCreateCommandResponse
    {
        public bool IsLogin { get; set; }
        public string Username { get; set; }
        public bool Succeed { get; set; }
    }
}
