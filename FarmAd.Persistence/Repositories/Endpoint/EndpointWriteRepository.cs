using FarmAd.Application.Repositories.Endpoint;
using FarmAd.Persistence.Contexts;

namespace FarmAd.Persistence.Repositories.Endpoint
{
    public class EndpointWriteRepository : WriteRepository<FarmAd.Domain.Entities.Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
