using FarmAd.Application.Abstractions.Services.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductCreateCommand
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest, ProductCreateCommandResponse>
    {
        private readonly IProductCreateServices _productCreateServices;

        public ProductCreateCommandHandler(IProductCreateServices productCreateServices)
        {
            _productCreateServices = productCreateServices;
        }
        public async Task<ProductCreateCommandResponse> Handle(ProductCreateCommandRequest request, CancellationToken cancellationToken)
        {
             await _productCreateServices.CreateImageFormFileAsync(request.FormFiles,request.ProductId);
            return new();
        }
    }
}
