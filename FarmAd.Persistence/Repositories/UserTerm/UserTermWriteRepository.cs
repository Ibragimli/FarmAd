using FarmAd.Application.Repositories.UserTerm;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.UserTerm
{
    public class UserTermWriteRepository : WriteRepository<FarmAd.Domain.Entities.UserTerm>, IUserTermWriteRepository
    {
        public UserTermWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
