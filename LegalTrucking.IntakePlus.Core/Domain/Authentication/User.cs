using Newtonsoft.Json;
using System;

using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;

using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Authentication
{
    public class User : AggregateRoot<UserDocument>
    {
        private String username;
        private String email;
        private String passwordHash;
        private DateTime createDt;
        
        public User(String username, String email, String hash, Id id) : 
            this(username, email, hash, DateTime.Now, new Version(),id )
        {

        }

        public User(String username, String email, String hash, DateTime creationTime, Version version, Id id) : base(id, version)
        {
            this.username = username;
            this.email = email;
            this.passwordHash = hash;
            this.createDt = creationTime;
        }

        public User() : base (new Id(), new Version()) { }

       
        public override void Load(UserDocument document)
        {
            id = new Id(document.Id);
            version = new Version(document.Version);
            username = document.Username;
            email = document.Email;
            passwordHash = document.PasswordHash;
            createDt = document.CreationTime;
        }

        public override UserDocument ToDocument()
        {
            return new UserDocument(
                Id,
                username,
                email,
                passwordHash,
                createDt,
                version);
        }

        public string PasswordHash {
            get { return this.passwordHash; }
        }

        public string Username
        {
            get { return this.username; }
        }


    }
}
