using LegalTrucking.IntakePlus.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Commands.Authentication
{
    public class LogoutCommand : Command
    {

        public LogoutCommand(Guid sessionId) : base(Guid.NewGuid())
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; internal set; }
    }
}
