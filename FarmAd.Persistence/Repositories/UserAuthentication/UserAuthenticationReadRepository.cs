using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.UserAuthentication
{
    public class UserAuthenticationReadRepository : ReadRepository<FarmAd.Domain.Entities.UserAuthentication>, IUserAuthenticationReadRepository
    {
        public UserAuthenticationReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
