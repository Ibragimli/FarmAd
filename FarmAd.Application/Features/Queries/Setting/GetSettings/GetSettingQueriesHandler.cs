using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Repositories.City;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Settings.GetSettings
{
    public class GetSettingsQueriesHandler : IRequestHandler<GetSettingsQueriesRequest, GetSettingsQueriesResponse>
    {
        private readonly ISettingIndexServices _settingIndexServices;


        public GetSettingsQueriesHandler(ISettingIndexServices settingIndexServices)
        {
            _settingIndexServices = settingIndexServices;
        }
        public async Task<GetSettingsQueriesResponse> Handle(GetSettingsQueriesRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = _settingIndexServices.GetAll(request.Search, request.Page, request.Size);
            return new()
            {
                Datas = datas,
                TotalCount = count
            };

        }
    }
}
