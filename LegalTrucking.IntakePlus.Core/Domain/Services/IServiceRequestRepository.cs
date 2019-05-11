using LegalTrucking.IntakePlus.Core.Adapters.Repositories;


namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public interface IServiceRequestRepository : IRepository<ServiceRequest, ServiceRequestDocument>
    {
    }
}
