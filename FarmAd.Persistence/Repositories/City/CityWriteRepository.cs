using FarmAd.Application.Repositories.City;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.City
{
    public class CityWriteRepository : WriteRepository<FarmAd.Domain.Entities.City>, ICityWriteRepository
    {
        public CityWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
