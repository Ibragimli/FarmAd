using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Application.DTOs;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductEditCommand
{
    public class ProductEditCommandHandler : IRequestHandler<ProductEditCommandRequest, ProductEditCommandResponse>
    {
        private readonly IProductEditServices _productEditServices;

        public ProductEditCommandHandler(IProductEditServices productEditServices)
        {
            _productEditServices = productEditServices;
        }
        public async Task<ProductEditCommandResponse> Handle(ProductEditCommandRequest request, CancellationToken cancellationToken)
        {
            _productEditServices.ProductEditCheck(request.ProductEditDto);
            await _productEditServices.ProductEdit(request.ProductEditDto);

            return new()
            {
                Succeed = true
            };
        }

    }
}
