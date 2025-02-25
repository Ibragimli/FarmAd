using FarmAd.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductDeleteCommand
{
    public class ProductDeleteCommandRequest : IRequest<ProductDeleteCommandResponse>
    {
        public int Id  { get; set; }

    }
}
