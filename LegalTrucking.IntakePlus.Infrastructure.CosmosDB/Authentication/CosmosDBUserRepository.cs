﻿using System;
using Microsoft.Azure.Documents;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Azure.Documents.Linq;
using System.Linq;

using LegalTrucking.IntakePlus.Core.Domain.Authentication;
using User = LegalTrucking.IntakePlus.Core.Domain.Authentication.User;
using LegalTrucking.IntakePlus.Core.Adapters.Exceptions;

namespace LegalTrucking.IntakePlus.Infrastructure.CosmosDB.Authentication
{
    public class CosmosDBUserRepository : CosmosDbRepository<User, UserDocument>, IUserRepository
    {
        private readonly ICosmosDbClientFactory _cosmosDbClientFactory;

        public CosmosDBUserRepository(ICosmosDbClientFactory factory) : base(factory) {
            _cosmosDbClientFactory = factory;
        }

        public override string CollectionName { get; } = "Users";

        public async Task<User> GetUserByUsernameAsync(string userName)
        {

            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var query = cosmosDbClient.Client.
                            CreateDocumentQuery<LegalTrucking.IntakePlus.Core.Domain.Authentication.User>(CollectionName, new SqlQuerySpec()
                {

                    QueryText = "SELECT * FROM Users U WHERE U.Username = @username",
                    Parameters = new SqlParameterCollection()
               {
                   new SqlParameter("@username", userName)
               }

                });

                var results = await query.AsDocumentQuery()
                                         .ExecuteNextAsync<LegalTrucking.IntakePlus.Core.Domain.Authentication.User>();
                var document = results.FirstOrDefault();

                var dataObject = JsonConvert.DeserializeObject<User>(document.ToString());
                var aggregate = new User();
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

        public override PartitionKey ResolvePartitionKey(Guid aggregateId) => new PartitionKey(aggregateId);
    }
}