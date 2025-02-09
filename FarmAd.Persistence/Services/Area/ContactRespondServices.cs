
using FarmAd.Application.DTOs.Area;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.ContactUs;
using Ferma.Service.Services.Interfaces.Area;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ferma.Service.Services.Implementations.Area
{
    public class ContactRespondServices : IContactRespondServices
    {
        private readonly IContactUsReadRepository _contactUsReadRepository;

        public ContactRespondServices(IContactUsReadRepository contactUsReadRepository)
        {
            _contactUsReadRepository = contactUsReadRepository;
        }

        public async Task<ReplyContactPostDto> RespondAnswer(int contactUsId, string RespondText)
        {
            var contactUs = await _contactUsReadRepository.GetAsync(x => x.Id == contactUsId);
            if (contactUs == null) throw new ItemNotFoundException("Xəta baş verdi");

            ReplyContactPostDto replyCommentPostDto = new ReplyContactPostDto
            {
                ContactUsId = contactUsId,
                ReplyText = RespondText,
                Email = contactUs.Email
            };
            return replyCommentPostDto;
        }

        public async Task<ContactUsReplyViewDto> RespondView(int contactUsId)
        {
            var contactUs = await _contactUsReadRepository.GetAsync(x => x.Id == contactUsId);
            if (contactUs == null) throw new ItemNotFoundException("Xəta baş verdi");
            ContactUsReplyViewDto contactUsReplyViewDto = new ContactUsReplyViewDto
            {
                ContactUsId = contactUsId,
                Fullname = contactUs.Fullname,
                ContactText = contactUs.Message,
                ReplyContactPostDto = new ReplyContactPostDto(),
            };
            return contactUsReplyViewDto;
        }
    }
}
