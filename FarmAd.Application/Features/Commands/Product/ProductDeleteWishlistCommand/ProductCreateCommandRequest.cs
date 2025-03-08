using FarmAd.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductDeleteWishlistCommand
{
    public class ProductDeleteWishlistCommandRequest : IRequest<ProductDeleteWishlistCommandResponse>
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public List<WishItemDto>? WishItems { get; set; }



    }
}
