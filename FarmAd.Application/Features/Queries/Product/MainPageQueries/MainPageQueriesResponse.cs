using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Product.MainPageQueries
{
    public class MainPageQueriesResponse
    {
        public List<FarmAd.Domain.Entities.Product> Datas { get; set; }
        public int TotalCount { get; set; }
    }
}
