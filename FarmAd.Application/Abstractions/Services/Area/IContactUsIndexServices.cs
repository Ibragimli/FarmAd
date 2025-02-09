using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferma.Service.Services.Interfaces.Area
{
    public interface IContactUsIndexServices
    {
        IQueryable<ContactUs> SearchCheck(string search);
    }
}
