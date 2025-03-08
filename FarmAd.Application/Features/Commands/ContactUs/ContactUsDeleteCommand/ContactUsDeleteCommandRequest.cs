using FarmAd.Application.DTOs.User;
using FarmAd.Application.Features.Commands.ContactUs.ContactUsCreateCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.ContactUs.ContactUsDeleteCommand
{
    public class ContactUsDeleteCommandRequest : IRequest<ContactUsDeleteCommandResponse>
    {
        public int Id { get; set; }
    }
}
