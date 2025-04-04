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

namespace FarmAd.Application.Features.Commands.Product.Area.ProductDisabledCommand
{
    public class ProductDisabledCommandHandler : IRequestHandler<ProductDisabledCommandRequest, ProductDisabledCommandResponse>
    {
        private readonly IAdminProductEditServices _adminProductEditServices;

        public ProductDisabledCommandHandler(IAdminProductEditServices adminProductEditServices)
        {
            _adminProductEditServices = adminProductEditServices;
        }
        public async Task<ProductDisabledCommandResponse> Handle(ProductDisabledCommandRequest request, CancellationToken cancellationToken)
        {
            await _adminProductEditServices.ProductDisabled(request.Id);

            return new()
            {
                Succeed = true
            };
        }

    }
}
