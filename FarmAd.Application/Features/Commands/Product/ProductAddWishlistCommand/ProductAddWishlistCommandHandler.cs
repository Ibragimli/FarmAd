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

namespace FarmAd.Application.Features.Commands.Product.ProductAddWishlistCommand
{
    public class ProductAddWishlistCommandHandler : IRequestHandler<ProductAddWishlistCommandRequest, ProductAddWishlistCommandResponse>
    {
        private readonly IProductWishlistAddServices _productWishlistAddServices;
        private readonly IUserService _userService;

        public ProductAddWishlistCommandHandler(IProductWishlistAddServices productWishlistAddServices, IUserService userService)
        {
            _productWishlistAddServices = productWishlistAddServices;
            _userService = userService;
        }
        public async Task<ProductAddWishlistCommandResponse> Handle(ProductAddWishlistCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWishlistAddServices.IsProduct(request.Id);
            bool isIdentity = _userService.IsIdendity(request.Username);
            if (!isIdentity)
                _productWishlistAddServices.CookieAddWish(request.Id);

            else
            {
                var user = await _userService.GetAsync(x => x.UserName == request.Username);
                await _productWishlistAddServices.UserAddWish(request.Id, user);
            }

            return new()
            {
                Succeed = true
            };
        }

    }
}
