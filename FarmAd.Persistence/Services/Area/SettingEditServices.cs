
using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.Area;
using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Repositories.Setting;

namespace FarmAd.Persistence.Service.Area
{
    public class SettingEditServices : ISettingEditServices
    {
        private readonly ISettingReadRepository _settingReadRepository;
        private readonly ISettingWriteRepository _settingWriteRepository;
        private readonly IImageManagerService _settingImage;
        private readonly IWebHostEnvironment _env;

        public SettingEditServices(ISettingReadRepository settingReadRepository, ISettingWriteRepository settingWriteRepository,IImageManagerService settingImage, IWebHostEnvironment env)
        {
            _settingReadRepository = settingReadRepository;
            _settingWriteRepository = settingWriteRepository;
            _settingImage = settingImage;
            _env = env;
        }


        public async Task<SettingEditDto> GetSearch(int Id)
        {
            var setting = await _settingReadRepository.GetAsync(x => x.Id == Id);
            if (setting == null)
                throw new ItemNotFoundException("Parametr tapılmadı");


            SettingEditDto settingEditDto = new SettingEditDto
            {
                Id = setting.Id,
                Key = setting.Key,
                KeyImageFile = setting.KeyImageFile,
                Value = setting.Value,
            };
            return settingEditDto;
        }

        public async Task SettingEdit(SettingEditDto SettingEdit)
        {
            if (SettingEdit.Value == null)
                throw new ItemNotFoundException("Dəyər adı boş ola bilməz!");

            var lastSetting = await _settingReadRepository.GetAsync(x => x.Id == SettingEdit.Id);

            if (lastSetting == null)
                throw new ItemNotFoundException("Parametr tapilmadı!");

            if (SettingEdit.Value != null)
                lastSetting.Value = SettingEdit.Value;

            if (SettingEdit.KeyImageFile != null)
            {
                lastSetting.KeyImageFile = SettingEdit.KeyImageFile;
                _settingImage.ValidateProduct(lastSetting.KeyImageFile);
                _settingImage.DeleteFile(lastSetting.Value, "setting");
                lastSetting.Value = _settingImage.FileSave(lastSetting.KeyImageFile, "setting");
                lastSetting.ModifiedDate = DateTime.UtcNow.AddHours(4);
            }
            await _settingWriteRepository.SaveAsync();
        }

        public async Task<SettingEditDto> IsExists(int id)
        {
            var SettingExist = await _settingReadRepository.GetAsync(x => x.Id == id);
            if (SettingExist == null)
                throw new ItemNotFoundException("ERROR");
            SettingEditDto editDto = new SettingEditDto
            {
                Value = SettingExist.Value,
                Id = SettingExist.Id,
            };
            return editDto;
        }
    }
}
