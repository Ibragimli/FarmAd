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

namespace FarmAd.Application.Features.Commands.ContactUs.ContactUsRespondCommand
{
    public class ContactUsRespondCommandHandler : IRequestHandler<ContactUsRespondCommandRequest, ContactUsRespondCommandResponse>
    {
        private readonly IContactRespondServices _contactRespondServices;

        public ContactUsRespondCommandHandler(IContactRespondServices contactRespondServices)
        {
            _contactRespondServices = contactRespondServices;
        }
        public async Task<ContactUsRespondCommandResponse> Handle(ContactUsRespondCommandRequest request, CancellationToken cancellationToken)
        {
            await _contactRespondServices.RespondAnswer(request.Id, request.Text);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
