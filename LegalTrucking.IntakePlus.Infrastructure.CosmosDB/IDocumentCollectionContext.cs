using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Infrastructure.CosmosDB
{
    public interface IDocumentCollectionContext<in T, TDocument> where T : IAmAnAggregateRoot<TDocument> where TDocument : IAmADocument
    {
        string CollectionName { get; }
        PartitionKey ResolvePartitionKey(Guid entityId);
    }
}
