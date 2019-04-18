using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Domain.Common;

namespace LegalTrucking.IntakePlus.Core.Domain.Agents
{
    public class Agent : AggregateRoot
    {
        public Agent(String firstName, String lastName): base(Guid.NewGuid())
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public String FirstName { get; internal set; }
        public String LastName { get; internal set; }
    }
}
