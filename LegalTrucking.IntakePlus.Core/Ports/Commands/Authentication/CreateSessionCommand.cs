using LegalTrucking.IntakePlus.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Commands.Authentication
{
    public class CreateSessionCommand : Command
    {
        public CreateSessionCommand(Guid userId) : base(Guid.NewGuid())
        {
            User = userId;
        }

        public Guid User { get; internal set; }
    }
}
