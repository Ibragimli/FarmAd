using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.ContactUs.GetContactUsQueries
{
    public class GetContactUsQueriesHandler : IRequestHandler<GetContactUsQueriesRequest, GetContactUsQueriesResponse>
    {
        private readonly IContactUsIndexServices _contactUsIndexServices;

        public GetContactUsQueriesHandler(IContactUsIndexServices contactUsIndexServices)
        {
            _contactUsIndexServices = contactUsIndexServices;
        }
        public async Task<GetContactUsQueriesResponse> Handle(GetContactUsQueriesRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = await _contactUsIndexServices.SearchCheck(request.Name, request.Page, request.Size);
            return new()
            {
                Datas = datas,
                TotalCount = count,
            };
        }
    }
}
