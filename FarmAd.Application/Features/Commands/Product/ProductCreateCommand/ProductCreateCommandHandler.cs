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
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest, ProductCreateCommandResponse>
    {
        private readonly IProductCreateServices _productCreateServices;
        private readonly IUserService _userService;

        public ProductCreateCommandHandler(IProductCreateServices productCreateServices, IUserService userService)
        {
            _productCreateServices = productCreateServices;
            _userService = userService;
        }
        public async Task<ProductCreateCommandResponse> Handle(ProductCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var dto = request.ProductCreateDto;
            AppUser user = await _userService.GetAsync(x => x.UserName == dto.UserName);

            //User daxil olubsa
            if (_userService.IsIdendity(dto.UserName))
            {
                var feature = await _productCreateServices.CreateProductFeature(dto);
                var prd = await _productCreateServices.CreateProductForm(feature, dto.ImageFiles);
                await _productCreateServices.CreateImagesAsync(dto.ImageFiles, prd.Id);
                await _productCreateServices.CreateProductUserId(user.Id, prd.Id);
            }
            //user daxil olmayibsa
            else
                _productCreateServices.CreateProductCookie(dto.ImageFiles, dto);
            string username = _userService.IdentityUser();

            if (user == null)
                await _userService.CreateNewUser(dto.UserName, dto.Email, dto.Name);

            return new()
            {
                Username = username,
                Succeed = true
            };
        }
    }
}
