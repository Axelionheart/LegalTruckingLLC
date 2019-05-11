using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Agents
{
    public class Agent : AggregateRoot<AgentDocument>
    {
        public Agent(String firstName, String lastName, Version version, Id id): base(id, version)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public String FirstName { get; internal set; }
        public String LastName { get; internal set; }

        public override void Load(AgentDocument document)
        {
            this.id = new Id(document.Id);
            this.version = document.Version;
            this.FirstName = document.FirstName;
            this.LastName = document.LastName;
        }

        public override AgentDocument ToDocument()
        {
            return new AgentDocument(
                FirstName,
                LastName,
                Version,
                Id);
        }
    }
}
