using FarmAd.Application.DTOs.Area;
using FarmAd.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.Area.ProductEditCommand
{
    public class ProductEditCommandRequest : IRequest<ProductEditCommandResponse>
    {
        public FarmAd.Application.DTOs.Area.AdminProductEditPostDto AdminProductEditPostDto { get; set; }

    }
}
