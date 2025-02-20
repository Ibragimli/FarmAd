using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Setting.UpdateSetting
{
    public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommandRequest, UpdateSettingCommandResponse>
    {
        private readonly ISettingEditServices _SettingService;

        public UpdateSettingCommandHandler(ISettingEditServices SettingService)
        {
            _SettingService = SettingService;
        }
        public async Task<UpdateSettingCommandResponse> Handle(UpdateSettingCommandRequest request, CancellationToken cancellationToken)
        {
            await _SettingService.SettingEdit(request.SettingUpdateDto);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
