using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Adapters.Repositories
{
    public interface IRepository<T> where T : IAmAnAggregateRoot
    {
        T this[Guid id] { get; }
        IUnitOfWork UnitOfWork { set; }
        void Add(T aggregate);
        void Delete(T aggregate);
    }
}
