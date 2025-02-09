using FarmAd.Application.DTOs.Area;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ferma.Service.Services.Interfaces.Area
{
    public interface IContactRespondServices
    {
        public Task<ContactUsReplyViewDto> RespondView(int contactUsId);
        public Task<ReplyContactPostDto> RespondAnswer(int contactUsId, string RespondText);
    }
}
