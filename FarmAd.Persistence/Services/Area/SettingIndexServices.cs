using FarmAd.Application.Repositories.Setting;
using FarmAd.Domain.Entities;

using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.DTOs.Area;
using FarmAd.Application.Exceptions;

namespace FarmAd.Persistence.Service.Area
{
    public class SettingIndexServices : ISettingIndexServices
    {
        private readonly ISettingReadRepository _settingReadRepository;

        public SettingIndexServices(ISettingReadRepository settingReadRepository)
        {
            _settingReadRepository = settingReadRepository;
        }

        public async Task<SettingUpdateDto> GetSearch(int Id)
        {
            var setting = await _settingReadRepository.GetAsync(x => x.Id == Id);
            if (setting == null)
                throw new ItemNotFoundException("Parametr tapılmadı");


            SettingUpdateDto settingEditDto = new SettingUpdateDto
            {
                Id = setting.Id,
                Key = setting.Key,
                KeyImageFile = setting.KeyImageFile,
                Value = setting.Value,
            };
            return settingEditDto;
        }
        public (object, int) GetAll(string search, int page, int size)
        {
            var settingCount = _settingReadRepository.AsQueryable().Count();
            var SettingLast = _settingReadRepository.GetAllPagenated(page, size);
            if (search != null)
            {
                search = search.ToLower();
                if (search != null)
                    SettingLast = SettingLast.Where(i => EF.Functions.Like(i.Key, $"%{search}%"));
            }
            return (SettingLast, settingCount);
        }

    }
}
