﻿using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.City.GetCities
{
    public class GetCitiesQueriesResponse
    {
        public object Datas { get; set; }
        public int TotalCount { get; set; }
    }
}
