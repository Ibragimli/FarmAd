using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public object Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}
