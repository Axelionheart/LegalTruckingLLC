using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using System;
using LegalTrucking.IntakePlus.Core.Domain.Services;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Agents
{
    public class AgentDocument : IAmADocument
    {
        public AgentDocument()
        {
        }

        public AgentDocument(String firstName, String lastName, Version version, Id id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Id = id;
            this.Version = version;
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Version Version { get; set; }
        public Guid Id { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, FirstName: {1}, LastName: {2}, Version {3}", Id, FirstName, LastName,Version);
        }
    }

}