using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Abstractions.Services.User;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.ContactUs.ContactUsCreateCommand
{
    public class ContactUsCreateCommandHandler : IRequestHandler<ContactUsCreateCommandRequest, ContactUsCreateCommandResponse>
    {
        private readonly IContactUsServices _contactUsServices;

        public ContactUsCreateCommandHandler(IContactUsServices contactUsServices)
        {
            _contactUsServices = contactUsServices;
        }
        public async Task<ContactUsCreateCommandResponse> Handle(ContactUsCreateCommandRequest request, CancellationToken cancellationToken)
        {
            await _contactUsServices.CreateContactUs(request.ContactUsDto);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
