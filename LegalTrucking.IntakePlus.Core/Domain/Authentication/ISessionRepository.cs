using LegalTrucking.IntakePlus.Core.Adapters.Repositories;

namespace LegalTrucking.IntakePlus.Core.Domain.Authentication
{
    public interface ISessionRepository : IRepository<LoginSession, LoginSessionDocument> { }
}
