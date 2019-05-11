using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Adapters.Exceptions;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace LegalTrucking.IntakePlus.Infrastructure.CosmosDB
{
    public abstract class CosmosDbRepository<T, TDocument> :
        IRepository<T, TDocument>, 
        IDocumentCollectionContext<T, TDocument> 
        where T : IAmAnAggregateRoot<TDocument>, new() where TDocument : IAmADocument
    {
        private readonly ICosmosDbClientFactory _cosmosDbClientFactory;

        protected CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory)
        {
            _cosmosDbClientFactory = cosmosDbClientFactory;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.ReadDocumentAsync(id.ToString(), new RequestOptions
                {
                    PartitionKey = ResolvePartitionKey(id)
                });

                var dataObject = JsonConvert.DeserializeObject<TDocument>(document.ToString());
                var aggregate = new T();
                aggregate.Load(dataObject);
                return aggregate;

            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public async Task<T> AddAsync(T aggregate)
        {
            try
            {
                TDocument dto = aggregate.ToDocument();
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.CreateDocumentAsync(dto);
                return JsonConvert.DeserializeObject<T>(document.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new EntityAlreadyExistsException();
                }

                throw;
            }
        }

        public async Task UpdateAsync(T aggregate)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                await cosmosDbClient.ReplaceDocumentAsync(aggregate.Id.ToString(), aggregate);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public async Task DeleteAsync(T aggregate)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                await cosmosDbClient.DeleteDocumentAsync(aggregate.Id.ToString(), new RequestOptions
                {
                    PartitionKey = ResolvePartitionKey(aggregate.Id)
                });
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public abstract string CollectionName { get; }
        public virtual PartitionKey ResolvePartitionKey(Guid entityId) => null;
    }
}
