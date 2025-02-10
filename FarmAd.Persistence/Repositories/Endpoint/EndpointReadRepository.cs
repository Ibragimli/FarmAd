using FarmAd.Application.Repositories.Endpoint;
using FarmAd.Persistence.Contexts;

namespace FarmAd.Persistence.Repositories.Endpoint
{
    public class EndpointReadRepository : ReadRepository<FarmAd.Domain.Entities.Endpoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
