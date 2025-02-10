using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Repositories.ContactUs;

namespace FarmAd.Persistence.Services.User
{
    public class ContactUsServices : IContactUsServices
    {
        private readonly IContactUsWriteRepository _contactUsWriteRepository;

        public ContactUsServices(IContactUsWriteRepository contactUsWriteRepository)
        {
            _contactUsWriteRepository = contactUsWriteRepository;
        }
        public void CheckContactUs(ContactUsDto contactUsDto)
        {
            if (contactUsDto == null)
                throw new ItemNotFoundException("Error404");
        }

        public async Task CreateContactUs(ContactUsDto contactUsDto)
        {
            ContactUs contact = new ContactUs
            {
                Email = contactUsDto.Email,
                Fullname = contactUsDto.FullName,
                Subject = contactUsDto.Subject,
                Message = contactUsDto.Message,
            };
            await _contactUsWriteRepository.AddAsync(contact);
            await _contactUsWriteRepository.SaveAsync();
        }
    }
}
