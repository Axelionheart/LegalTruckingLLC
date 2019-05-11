using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Adapters.Repositories
{
    public interface IAmAnAggregateRoot<TDocument> where TDocument : IAmADocument
    {
        Id Id { get; }
        Version Version { get; }
        void Load(TDocument document);
        Version Lock(Version expectedVersion);
        TDocument ToDocument();
    }
}
