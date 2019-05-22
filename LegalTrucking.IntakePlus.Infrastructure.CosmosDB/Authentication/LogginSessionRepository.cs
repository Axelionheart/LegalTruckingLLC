using LegalTrucking.IntakePlus.Core.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Infrastructure.CosmosDB.Authentication
{
    public class LogginSessionRepository : ISessionRepository
    {
        public Task<LoginSession> AddAsync(LoginSession entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(LoginSession entity)
        {
            throw new NotImplementedException();
        }

        public Task<LoginSession> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(LoginSession entity)
        {
            throw new NotImplementedException();
        }
    }
}
