using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using System;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Authentication
{
    public class LoginSessionDocument : IAmADocument
    {
        public LoginSessionDocument() { }

        public LoginSessionDocument(Id sessionId, Guid userId, DateTime createDt, DateTime? logout, Version version)
        {
            SessionId = sessionId;
            UserId = userId;
            CreationTime = createDt;
            LogoutTime = logout;
            Version = version;
        }

        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public int Version { get; set; }
    }
}