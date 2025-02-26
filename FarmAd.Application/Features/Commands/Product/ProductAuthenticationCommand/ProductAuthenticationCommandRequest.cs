using FarmAd.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductAuthenticationCommand
{
    public class ProductAuthenticationCommandRequest : IRequest<ProductAuthenticationCommandResponse>
    {
        public ProductAuthenticationDto ProductAuthenticationDto { get; set; }

    }
}
