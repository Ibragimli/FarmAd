using FarmAd.Domain.Entities;
using FarmAd.Application.Abstractions.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Repositories.UserTerm;

namespace FarmAd.Persistence.Service.User
{
    public class UserTermIndexServices : IUserTermIndexServices
    {
        private readonly IUserTermReadRepository _userTermReadRepository;

        public UserTermIndexServices(IUserTermReadRepository userTermReadRepository)
        {
            _userTermReadRepository = userTermReadRepository;
        }
        public async Task<List<UserTerm>> UserTerms()
        {
            var terms = await _userTermReadRepository.GetAllAsync(x => !x.IsDelete);
            return terms.ToList();
        }
    }
}
