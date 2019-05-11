using System;

using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Authentication
{
    public class UserDocument : IAmADocument
    {

        public UserDocument(Id userId, String username, String email, String hash, DateTime createDate, Version version)
        {
            Id = userId;
            Username = username;
            Email = email;
            PasswordHash = hash;
            CreationTime = createDate;
            Version = version;
        }

        public UserDocument()
        {
        }

        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime CreationTime { get; set; }
        public int Version { get; set; }
    }
}
