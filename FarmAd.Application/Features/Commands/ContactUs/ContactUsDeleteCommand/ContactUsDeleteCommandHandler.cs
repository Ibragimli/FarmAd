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

namespace FarmAd.Application.Features.Commands.ContactUs.ContactUsDeleteCommand
{
    public class ContactUsDeleteCommandHandler : IRequestHandler<ContactUsDeleteCommandRequest, ContactUsDeleteCommandResponse>
    {
        private readonly IContactUsDeleteServices _contactUsDeleteServices;

        public ContactUsDeleteCommandHandler(IContactUsDeleteServices contactUsDeleteServices)
        {
            _contactUsDeleteServices = contactUsDeleteServices;
        }
        public async Task<ContactUsDeleteCommandResponse> Handle(ContactUsDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            await _contactUsDeleteServices.ContactUsDelete(request.Id);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
