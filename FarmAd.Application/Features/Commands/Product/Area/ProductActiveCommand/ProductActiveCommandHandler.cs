using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Application.DTOs;
using FarmAd.Application.Features.Commands.Product.Area.ProductActiveCommand;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.Area.AdminProductEditPostDto
{
    public class AdminProductEditPostDtoHandler : IRequestHandler<ProductActiveCommandRequest, ProductActiveCommandResponse>
    {
        private readonly IAdminProductEditServices _adminProductEditServices;

        public AdminProductEditPostDtoHandler(IAdminProductEditServices adminProductEditServices)
        {
            _adminProductEditServices = adminProductEditServices;
        }
        public async Task<ProductActiveCommandResponse> Handle(ProductActiveCommandRequest request, CancellationToken cancellationToken)
        {
            await _adminProductEditServices.ProductActive(request.Id);

            return new()
            {
                Succeed = true
            };
        }

    }
}
