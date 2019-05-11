using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using Newtonsoft.Json;
using System;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Authentication
{
    public class LoginSession : AggregateRoot<LoginSessionDocument>
    {
        private Id user;
        private DateTime created;
        private DateTime? loggedOut;

        public LoginSession(Id user, DateTime created) : this(user, created, new Version(), new Id())
        {

        }

        public LoginSession(Id user, DateTime created, Version version, Id id) : base(id, version)
        {
            this.user = user;
            this.created = created;
        }

        public LoginSession() : base(new Id(), new Version()) { }

        public void LogOut()
        {
            this.loggedOut = DateTime.Now;
        }

        public override void Load(LoginSessionDocument document)
        {
            user = new Id(document.UserId);
            created = document.CreationTime;
            loggedOut = document.LogoutTime;
        }

        public bool IsLoggedOut()
        {
            return this.loggedOut.HasValue;
        }

        public override LoginSessionDocument ToDocument()
        {
            return new LoginSessionDocument(Id, user, created, loggedOut, version);
        }
    }
}
