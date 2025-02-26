using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductAuthenticationCommand
{
    public class ProductAuthenticationCommandHandler : IRequestHandler<ProductAuthenticationCommandRequest, ProductAuthenticationCommandResponse>
    {
        private readonly IProductCreateServices _productCreateServices;
        private readonly IUserService _userService;
        private readonly IUserAuthenticationService _authenticationService;

        public ProductAuthenticationCommandHandler(IProductCreateServices productCreateServices, IUserService userService, IUserAuthenticationService authenticationService)
        {
            _productCreateServices = productCreateServices;
            _userService = userService;
            _authenticationService = authenticationService;
        }
        public async Task<ProductAuthenticationCommandResponse> Handle(ProductAuthenticationCommandRequest request, CancellationToken cancellationToken)
        {
            var dto = request.ProductAuthenticationDto;
            var prdCookie = await _productCreateServices.GetProductFromRedisAsync(dto.Username);

            await _authenticationService.CheckAuthenticationAsync(dto.Code, dto.Username, prdCookie.ImageFilesStr);

            var images = await _productCreateServices.GetProductFromRedisAsync(dto.Username);
            var feature = await _productCreateServices.CreateProductFeature(prdCookie);
            var prd = await _productCreateServices.CreateProductForm(feature, prdCookie.ImageFiles);
            await _productCreateServices.CreateImagesAsync(images.ImageFilesStr,images.ImagePathStr,prd.Id,dto.Username);
            var user = await _userService.GetAsync(x => x.UserName == dto.Username);
            await _productCreateServices.CreateProductUserId(user.Id, prd.Id);
            return new()
            {
                Succeed = true,
            };
        }
    }
}
