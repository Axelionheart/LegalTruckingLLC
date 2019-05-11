using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Domain.Common;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Ports.Commands.Services
{
    public class CompleteServiceRequestCommand : Command
    {
        public CompleteServiceRequestCommand(Guid requestId, Guid byWho, Guid changedByWho, DateTime on, int version) : base(Guid.NewGuid())
        {
            RequestId = requestId;
            CompletedBy = byWho;
            ChangedBy = changedByWho;
            CompletedOn = on;
            Version = new Version(version);
        }


        public Guid RequestId { get; set; }
        public Guid CompletedBy { get; set; }
        public DateTime CompletedOn { get; set; }
        public Guid ChangedBy { get; set; }
        public Version Version { get; set; }
    }
}
