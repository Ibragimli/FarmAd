
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.ContactUs;
using Ferma.Service.Services.Interfaces.Area;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ferma.Service.Services.Implementations.Area
{
    public class ContactUsDeleteServices : IContactUsDeleteServices
    {
        private readonly IContactUsReadRepository _contactUsReadRepository;
        private readonly IContactUsWriteRepository _contactUsWriteRepository;

        public ContactUsDeleteServices(IContactUsReadRepository contactUsReadRepository, IContactUsWriteRepository contactUsWriteRepository)
        {
            _contactUsReadRepository = contactUsReadRepository;
            _contactUsWriteRepository = contactUsWriteRepository;
        }

        public async Task ContactUsDelete(int id)
        {
            if (!await _contactUsReadRepository.IsExistAsync(x => x.Id == id)) throw new ItemNotFoundException("Əlaqə məlumatları  tapılmadı!");

            var ContactUs = await _contactUsReadRepository.GetAsync(x => x.Id == id);

            _contactUsWriteRepository.Remove(ContactUs);
            await _contactUsWriteRepository.SaveAsync();
        }
    }
}
