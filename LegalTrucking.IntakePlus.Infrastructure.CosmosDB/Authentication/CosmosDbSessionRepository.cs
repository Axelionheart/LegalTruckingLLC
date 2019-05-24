using LegalTrucking.IntakePlus.Core.Adapters.Exceptions;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Authentication;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using LoginSession = LegalTrucking.IntakePlus.Core.Domain.Authentication.LoginSession;

namespace LegalTrucking.IntakePlus.Infrastructure.CosmosDB.Authentication
{
    public class CosmosDBSessionRepository : CosmosDbRepository<LoginSession, LoginSessionDocument>, ISessionRepository
    {
        private readonly ICosmosDbClientFactory _cosmosDbClientFactory;

        public CosmosDBSessionRepository(ICosmosDbClientFactory factory) : base(factory)
        {
            _cosmosDbClientFactory = factory;
        }

        public override string CollectionName => "Sessions";
    }
}
