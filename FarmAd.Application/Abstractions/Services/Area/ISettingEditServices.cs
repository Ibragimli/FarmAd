using FarmAd.Application.DTOs.Area;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface ISettingEditServices
    {
        Task SettingEdit(SettingEditDto SettingEdit);
        Task<SettingEditDto> IsExists(int id);
        Task<SettingEditDto> GetSearch(int Id);
    }
}
