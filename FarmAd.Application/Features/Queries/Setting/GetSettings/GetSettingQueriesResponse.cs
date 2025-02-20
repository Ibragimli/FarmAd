using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Settings.GetSettings
{
    public class GetSettingsQueriesResponse
    {
        public object Datas { get; set; }
        public int TotalCount { get; set; }
    }
}
