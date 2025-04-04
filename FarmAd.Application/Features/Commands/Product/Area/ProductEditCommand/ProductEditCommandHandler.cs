using FarmAd.Application.Abstractions.Services.Area;
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

namespace FarmAd.Application.Features.Commands.Product.Area.ProductEditCommand
{
    public class ProductEditCommandHandler : IRequestHandler<ProductEditCommandRequest, ProductEditCommandResponse>
    {
        private readonly IAdminProductEditServices _adminProductEditServices;

        public ProductEditCommandHandler(IAdminProductEditServices adminProductEditServices)
        {
            _adminProductEditServices = adminProductEditServices;
        }
        public async Task<ProductEditCommandResponse> Handle(ProductEditCommandRequest request, CancellationToken cancellationToken)
        {
            await _adminProductEditServices.CheckPostEdit(request.AdminProductEditPostDto);
            await _adminProductEditServices.EditProduct(request.AdminProductEditPostDto);

            return new()
            {
                Succeed = true
            };
        }

    }
}
