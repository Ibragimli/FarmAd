using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductCreateCommand
{
    public class ProductLoginCreateCommandHandler : IRequestHandler<ProductLoginCreateCommandRequest, ProductLoginCreateCommandResponse>
    {
        private readonly IProductCreateServices _productCreateServices;
        private readonly IUserService _userService;
        private readonly IUserAuthenticationService _authenticationService;

        public ProductLoginCreateCommandHandler(IProductCreateServices productCreateServices, IUserService userService, IUserAuthenticationService authenticationService)
        {
            _productCreateServices = productCreateServices;
            _userService = userService;
            _authenticationService = authenticationService;
        }
        public async Task<ProductLoginCreateCommandResponse> Handle(ProductLoginCreateCommandRequest request, CancellationToken cancellationToken)
        {

            var prdCookie = _productCreateServices.GetProductCookie();
            var images = _productCreateServices.GetImageFilesCookie();
            var feature = await _productCreateServices.CreateProductFeature(prdCookie);
            var prd = await _productCreateServices.CreateProductForm(feature, prdCookie.ImageFiles);
            var user = await _userService.GetAsync(x => x.UserName == request.ProductCreateDto.UserName);
            await _productCreateServices.CreateProductUserId(user.Id, prd.Id);
            string username = _userService.IdentityUser();
            return new()
            {
                Succeed = true,
                Username = username
            };
        }
    }
}
