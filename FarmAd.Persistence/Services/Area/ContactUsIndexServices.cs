using FarmAd.Application.Repositories.ContactUs;
using FarmAd.Domain.Entities;

using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Service.Area
{
    public class ContactUsIndexServices : IContactUsIndexServices
    {
        private readonly IContactUsReadRepository _contactUsReadRepository;

        public ContactUsIndexServices(IContactUsReadRepository contactUsReadRepository)
        {
            _contactUsReadRepository = contactUsReadRepository;
        }
        public IQueryable<ContactUs> SearchCheck(string search)
        {
            var contactUsLast = _contactUsReadRepository.AsQueryable();

            if (search != null)
            {
                search = search.ToLower();
                //categorySearch
                if (search != null)
                    contactUsLast = contactUsLast.Where(i => EF.Functions.Like(i.Subject, $"%{search}%"));
            }
            return contactUsLast;
        }

    }

}
