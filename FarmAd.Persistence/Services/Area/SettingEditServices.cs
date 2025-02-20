
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
using FarmAd.Application.Abstractions.Storage.Local;
using FarmAd.Application.Abstractions.Storage;

namespace FarmAd.Persistence.Service.Area
{
    public class SettingEditServices : ISettingEditServices
    {
        private readonly ISettingReadRepository _settingReadRepository;
        private readonly IImageManagerService _ımageManagerService;
        private readonly ISettingWriteRepository _settingWriteRepository;
        private readonly IStorageService _storageService;

        public SettingEditServices(ISettingReadRepository settingReadRepository, IImageManagerService ımageManagerService, ISettingWriteRepository settingWriteRepository, IStorageService storageService)
        {
            _settingReadRepository = settingReadRepository;
            _ımageManagerService = ımageManagerService;
            _settingWriteRepository = settingWriteRepository;
            _storageService = storageService;
        }

        public Task<SettingUpdateDto> IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SettingEdit(SettingUpdateDto SettingEdit)
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
                _ımageManagerService.ValidateProduct(lastSetting.KeyImageFile);
                await _storageService.DeleteAsync("files\\settings", lastSetting.Value);
                var (filename, path) = _storageService.UploadAsync("files\\settings", lastSetting.KeyImageFile);
                lastSetting.Value = filename;
                lastSetting.ImagePath = path;
                lastSetting.ModifiedDate = DateTime.UtcNow.AddHours(4);
            }
            await _settingWriteRepository.SaveAsync();
        }

        //public async Task<SettingEditDto> IsExists(int id)
        //{
        //    var SettingExist = await _settingReadRepository.GetAsync(x => x.Id == id);
        //    if (SettingExist == null)
        //        throw new ItemNotFoundException("ERROR");
        //    SettingEditDto editDto = new SettingEditDto
        //    {
        //        Value = SettingExist.Value,
        //        Id = SettingExist.Id,
        //    };
        //    return editDto;
        //}


    }
}
