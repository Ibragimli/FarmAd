using FarmAd.Application.DTOs.Area;
using FarmAd.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.Area.ProductDisabledCommand
{
    public class ProductDisabledCommandRequest : IRequest<ProductDisabledCommandResponse>
    {
        public int Id { get; set; }

    }
}
