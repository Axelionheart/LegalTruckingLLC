using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Adapters.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Add(dynamic entity);
        void Delete(dynamic entity);
        T Load<T>(Guid id);
        void Commit();
    }
}
