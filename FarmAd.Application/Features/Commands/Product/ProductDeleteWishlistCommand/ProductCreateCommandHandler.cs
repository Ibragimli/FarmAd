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

namespace FarmAd.Application.Features.Commands.Product.ProductDeleteWishlistCommand
{
    public class ProductDeleteWishlistCommandHandler : IRequestHandler<ProductDeleteWishlistCommandRequest, ProductDeleteWishlistCommandResponse>
    {
        private readonly IProductWishlistDeleteServices _productWishlistDeleteServices;
        private readonly IUserService _userService;

        public ProductDeleteWishlistCommandHandler(IProductWishlistDeleteServices productWishlistDeleteServices, IUserService userService)
        {
            _productWishlistDeleteServices = productWishlistDeleteServices;
            _userService = userService;
        }
        public async Task<ProductDeleteWishlistCommandResponse> Handle(ProductDeleteWishlistCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWishlistDeleteServices.IsProduct(request.Id);
            bool isIdentity = _userService.IsIdendity(request.Username);

            if (isIdentity)
            {
                var user = await _userService.GetAsync(x => x.UserName == request.Username);
                await _productWishlistDeleteServices.UserDeleteWish(request.Id, user);
            }
            else
                _productWishlistDeleteServices.CookieDeleteWish(request.Id, request.WishItems);

            return new()
            {
                Succeed = true
            };
        }

    }
}
