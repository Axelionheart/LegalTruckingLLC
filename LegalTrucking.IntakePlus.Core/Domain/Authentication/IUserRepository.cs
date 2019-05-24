using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using System.Threading.Tasks;


namespace LegalTrucking.IntakePlus.Core.Domain.Authentication
{
    public interface IUserRepository : IRepository<User, UserDocument> {
        Task<UserDocument> GetUserByUsernameAsync(string userName);
    }
}
