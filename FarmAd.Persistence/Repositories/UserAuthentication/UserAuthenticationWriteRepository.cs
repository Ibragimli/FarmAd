using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.UserAuthentication
{
    public class UserAuthenticationWriteRepository : WriteRepository<FarmAd.Domain.Entities.UserAuthentication>, IUserAuthenticationWriteRepository
    {
        public UserAuthenticationWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
