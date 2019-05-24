using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LegalTrucking.IntakePlus.Infrastructure.CosmosDB
{
    public class CosmosDbClientFactory : ICosmosDbClientFactory
    {
        private readonly string _databaseId;
        private readonly List<string> _collectionNames;
        private readonly IDocumentClient _documentClient;

        public CosmosDbClientFactory(string databaseId, List<string> collectionNames, IDocumentClient documentClient)
        {
            _databaseId = databaseId ?? throw new ArgumentNullException(nameof(databaseId));
            _collectionNames = collectionNames ?? throw new ArgumentNullException(nameof(collectionNames));
            _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
        }

        public ICosmosDbClient GetClient(string collectionName)
        {
            if (!_collectionNames.Contains(collectionName))
            {
                throw new ArgumentException($"Unable to find collection: {collectionName}");
            }

            return new CosmosDbClient(_databaseId, collectionName, _documentClient);
        }

        public async Task EnsureDbSetupAsync()
        {
            await CreateDatabaseIfNotExistsAsync();

            foreach (var collectionName in _collectionNames)
            {
                await CreateCollectionIfNotExistsAsync(collectionName);
            }
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await _documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_databaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _documentClient.CreateDatabaseAsync(new Database { Id = _databaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync(string collectionName)
        {
            try
            {
              await _documentClient.ReadDocumentCollectionAsync(
              UriFactory.CreateDocumentCollectionUri(_databaseId, collectionName));
               
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                   
                        await _documentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_databaseId),
                        new DocumentCollection { Id = collectionName },
                        new RequestOptions { OfferThroughput = 1000 });
                                        
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
