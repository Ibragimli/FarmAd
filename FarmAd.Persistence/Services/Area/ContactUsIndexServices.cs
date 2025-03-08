using FarmAd.Application.Repositories.ContactUs;
using FarmAd.Domain.Entities;

using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FarmAd.Persistence.Service.Area
{
    public class ContactUsIndexServices : IContactUsIndexServices
    {
        private readonly IContactUsReadRepository _contactUsReadRepository;

        public ContactUsIndexServices(IContactUsReadRepository contactUsReadRepository)
        {
            _contactUsReadRepository = contactUsReadRepository;
        }
        public async Task<(object, int)> SearchCheck(string search, int page, int size)
        {
            int count = await _contactUsReadRepository.GetTotalCountAsync(x => !x.IsDelete);
            var contactUsLast = _contactUsReadRepository.GetAllPagenated(page, size);

            if (search != null)
            {
                search = search.ToLower();
                //categorySearch
                if (search != null)
                    contactUsLast = contactUsLast.Where(i => EF.Functions.Like(i.Subject, $"%{search}%"));
            }
            return (contactUsLast, count);

        }

    }

}
