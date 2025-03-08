using FarmAd.Application.DTOs.User;
using FarmAd.Application.Features.Commands.ContactUs.ContactUsCreateCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.ContactUs.ContactUsRespondCommand
{
    public class ContactUsRespondCommandRequest : IRequest<ContactUsRespondCommandResponse>
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
