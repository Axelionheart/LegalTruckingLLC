using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Domain.Authentication;

namespace CosmosDBRepository.Users
{
    public class UserPersistence : IUserPersistence
    {
        private const string USERS_DOCUMENT_COLLECTION_ID = "Users";
        private const string AUTHS_DOCUMENT_COLLECTION_ID = "UserAuthentications";
        private const string SESSIONS_DOCUMENT_COLLECTION_ID = "Sessions";

        private DocumentClient _client;
        private string _databaseId;

        public UserPersistence(DocumentClient client, string databaseId)
        {
            _client = client;
            _databaseId = databaseId;
        }

        public async Task EnsureSetupAsync()
        {

            var databaseUri = UriFactory.CreateDatabaseUri(_databaseId);

            // Collections
            await _client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, new DocumentCollection() { Id = SESSIONS_DOCUMENT_COLLECTION_ID });
            await _client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, new DocumentCollection() { Id = AUTHS_DOCUMENT_COLLECTION_ID });

            var users = new DocumentCollection();

            users.Id = USERS_DOCUMENT_COLLECTION_ID;

            users.UniqueKeyPolicy = new UniqueKeyPolicy()
            {

                UniqueKeys = new Collection<UniqueKey>()
                {
                    new UniqueKey{ Paths = new Collection<string>{ "/Username" } }
                }

            };

            await _client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, users);

        }

        // Users 
        public async Task<LegalTrucking.IntakePlus.Core.Domain.Authentication.User> CreateUserAsync(LegalTrucking.IntakePlus.Core.Domain.Authentication.User user)
        {

            var result = await _client.CreateDocumentAsync(GetUsersCollectionUri(), user, new RequestOptions() { });
            return JsonConvert.DeserializeObject<LegalTrucking.IntakePlus.Core.Domain.Authentication.User>(result.Resource.ToString());

        }

        public async Task<LegalTrucking.IntakePlus.Core.Domain.Authentication.User> GetUserAsync(string userId)
        {

            var result = await _client.ReadDocumentAsync<LegalTrucking.IntakePlus.Core.Domain.Authentication.User>(UriFactory.CreateDocumentUri(_databaseId, USERS_DOCUMENT_COLLECTION_ID, userId));
            return result.Document;
        }

        public async Task<LegalTrucking.IntakePlus.Core.Domain.Authentication.User> GetUserBySessionIdAsync(string sessionId)
        {

            var query = _client.CreateDocumentQuery<string>(GetSessionsCollectionUri(), new SqlQuerySpec()
            {
                QueryText = "SELECT VALUE S.UserId FROM Sessions S WHERE S.id = @sessionId",
                Parameters = new SqlParameterCollection()
               {
                   new SqlParameter("@sessionId", sessionId)
               }

            });

            var results = await query.AsDocumentQuery()
                                     .ExecuteNextAsync<string>();

            if (results.Count == 0)
            {
                return null;
            }
            else
            {
                return await GetUserAsync(results.Single());
            }

        }

        public async Task<LegalTrucking.IntakePlus.Core.Domain.Authentication.User> GetUserByUsernameAsync(string userName)
        {

            var query = _client.CreateDocumentQuery<LegalTrucking.IntakePlus.Core.Domain.Authentication.User>(GetUsersCollectionUri(), new SqlQuerySpec()
            {

                QueryText = "SELECT * FROM Users U WHERE U.Username = @username",
                Parameters = new SqlParameterCollection()
               {
                   new SqlParameter("@username", userName)
               }

            });

            var results = await query.AsDocumentQuery()
                                     .ExecuteNextAsync<LegalTrucking.IntakePlus.Core.Domain.Authentication.User>();
            return results.FirstOrDefault();
        }

        public async Task<LoginSession> CreateSessionAsync(LoginSession session)
        {
            var result = await _client.CreateDocumentAsync(GetSessionsCollectionUri(), session);
            return JsonConvert.DeserializeObject<LoginSession>(result.Resource.ToString());
        }

        public async Task<LoginSession> GetSessionAsync(string sessionId)
        {
            var result = await _client.ReadDocumentAsync<LoginSession>(UriFactory.CreateDocumentUri(_databaseId, SESSIONS_DOCUMENT_COLLECTION_ID, sessionId));
            return result.Document;
        }

        public async Task UpdateSessionAsync(LoginSession session)
        {
            await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, SESSIONS_DOCUMENT_COLLECTION_ID, session.Id), session);
        }


        public Uri GetUsersCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(_databaseId, USERS_DOCUMENT_COLLECTION_ID);
        }

        public Uri GetAuthenticationsCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(_databaseId, AUTHS_DOCUMENT_COLLECTION_ID);
        }

        public Uri GetSessionsCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(_databaseId, SESSIONS_DOCUMENT_COLLECTION_ID);
        }
    }
}
