using FarmAd.Application.Repositories.ContactUs;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.ContactUs
{
    public class ContactUsReadRepository : ReadRepository<FarmAd.Domain.Entities.ContactUs>, IContactUsReadRepository
    {
        public ContactUsReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
