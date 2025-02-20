using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface ISettingIndexServices
    {
        public (object, int) GetAll(string search, int page, int size);

    }
}
