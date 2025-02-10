using FarmAd.Application.Repositories.Setting;
using FarmAd.Domain.Entities;

using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Service.Area
{
    public class SettingIndexServices : ISettingIndexServices
    {
        private readonly ISettingReadRepository _settingReadRepository;

        public SettingIndexServices(ISettingReadRepository settingReadRepository)
        {
            _settingReadRepository = settingReadRepository;
        }


        public IQueryable<Setting> SearchCheck(string search)
        {
            var SettingLast = _settingReadRepository.AsQueryable();
            if (search != null)
            {
                search = search.ToLower();
                if (search != null)
                    SettingLast = SettingLast.Where(i => EF.Functions.Like(i.Key, $"%{search}%"));
            }
            return SettingLast;
        }

    }
}
