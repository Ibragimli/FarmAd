using FarmAd.Application.Repositories.ContactUs;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.ContactUs
{
    public class ContactUsWriteRepository : WriteRepository<FarmAd.Domain.Entities.ContactUs>, IContactUsWriteRepository
    {
        public ContactUsWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
