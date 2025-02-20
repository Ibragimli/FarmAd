using FarmAd.Application.DTOs.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Setting.UpdateSetting
{
    public class UpdateSettingCommandRequest : IRequest<UpdateSettingCommandResponse>
    {
        public SettingUpdateDto SettingUpdateDto { get; set; }
    }
}
