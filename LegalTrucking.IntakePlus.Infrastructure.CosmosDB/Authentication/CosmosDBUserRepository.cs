using System;
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

        public async Task<User> AddUserAsync(User user)
        {
            var result = GetUserByUsernameAsync(user.Username);
            if (result.Result == null)
                return await AddAsync(user);
            else
                throw new EntityAlreadyExistsException();
        }

        public async Task<User> GetUserByUsernameAsync(string userName)
        {

            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var query = cosmosDbClient.Client.
                                CreateDocumentQuery<UserDocument>(cosmosDbClient.GetCollectionUri(), new SqlQuerySpec()
                                {

                                    QueryText = "SELECT * FROM Users U WHERE U.Username = @username",
                                    Parameters = new SqlParameterCollection()
                   {
                   new SqlParameter("@username", userName)
                   }

                                });

                var results = await query.AsDocumentQuery()
                                         .ExecuteNextAsync<UserDocument>();
                if (results.Count > 0)
                {
                    var dataObject = JsonConvert.DeserializeObject<UserDocument>(results.FirstOrDefault().ToString());
                    var aggregate = new User();
                    aggregate.Load(dataObject);
                    return aggregate;
                }
                else
                {
                    return null;
                }
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
