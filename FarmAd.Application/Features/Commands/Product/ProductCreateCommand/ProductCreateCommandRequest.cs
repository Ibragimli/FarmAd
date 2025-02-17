using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductCreateCommand
{
    public class ProductCreateCommandRequest : IRequest<ProductCreateCommandResponse>
    {
        public int ProductId { get; set; }
        public List<IFormFile>? FormFiles { get; set; }
    }
}
