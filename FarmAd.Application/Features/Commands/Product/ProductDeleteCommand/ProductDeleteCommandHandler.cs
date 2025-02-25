using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductDeleteCommand
{
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommandRequest, ProductDeleteCommandResponse>
    {
        private readonly IProductDeleteServices _productDeleteServices;
        private readonly IUserService _userService;

        public ProductDeleteCommandHandler(IProductDeleteServices productDeleteServices, IUserService userService)
        {
            _productDeleteServices = productDeleteServices;
            _userService = userService;
        }
        public async Task<ProductDeleteCommandResponse> Handle(ProductDeleteCommandRequest request, CancellationToken cancellationToken)
        {

            await _productDeleteServices.DeleteProduct(request.Id);

            return new()
            {
                Succeed = true
            };
        }
    }
}
