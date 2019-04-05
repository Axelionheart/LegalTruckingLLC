using CosmosDBRepository.Users;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDBRepository
{
    public class Persistence : IDisposable
    {
        private string _databaseId;
        private Uri _endpointUri;
        private string _primaryKey;

        private DocumentClient _client;
        private bool _isDisposing;

        public Persistence(Uri endpointUri, string primaryKey, string databaseId)
        {

            _databaseId = databaseId;
            _endpointUri = endpointUri;
            _primaryKey = primaryKey;

            _client = new DocumentClient(endpointUri, primaryKey);

            _client.OpenAsync();

           Users = new UserPersistence(_client, _databaseId);
        }
               
        public UserPersistence Users { get; private set; }

        public async Task EnsureSetupAsync()
        {

            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _databaseId });

            await Users.EnsureSetupAsync();
        }

        public void Dispose()
        {
            if (!_isDisposing)
            {
                _isDisposing = true;

                if (_client != null)
                {
                    _client.Dispose();
                }
            }

        }

    }
}
