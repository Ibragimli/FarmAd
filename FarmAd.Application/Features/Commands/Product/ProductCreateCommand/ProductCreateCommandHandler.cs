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
            bool isLogin = true;
            var dto = request.ProductCreateDto;
            AppUser user = await _userService.GetAsync(x => x.UserName == dto.UserName);
            string username = _userService.IdentityUser();

            // User yaradilmiyibsa
            if (user == null)
            {
                await _userService.CreateNewUser(dto.UserName, dto.Email, dto.Name);
                user = await _userService.GetAsync(x => x.UserName == dto.UserName); // Kullanıcıyı tekrar getir
            }

            // Kullanıcı giriş yaptıysa
            if (_userService.IsIdendity(dto.UserName))
            {
                var feature = await _productCreateServices.CreateProductFeature(dto);
                var prd = await _productCreateServices.CreateProductForm(feature, dto.ImageFiles);
                await _productCreateServices.CreateImagesAsync(dto.ImageFiles, prd.Id);
                await _productCreateServices.CreateProductUserId(user.Id, prd.Id);
            }
            else // Kullanıcı giriş yapmadıysa
            {
                await _productCreateServices.CreateProductRedisAsync(dto.ImageFiles, dto);
                await _productCreateServices.CreateOTPCode(dto.UserName);
                isLogin = false;
            }

            return new()
            {
                IsLogin = isLogin,
                Username = username,
                Succeed = true
            };
        }

    }
}
