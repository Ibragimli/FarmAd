using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferma.Service.Services.Interfaces.Area
{
   public interface IAdminUserTermServices
    {
        public IQueryable<UserTerm> GetUserTerms(string name);
        public Task<UserTerm> GetUserTerm(int id);
        public Task UserTermCreate(UserTerm UserTerm);
        public Task UserTermEdit(UserTerm UserTerm);
        public Task UserTermDelete(int id);
    }
}
