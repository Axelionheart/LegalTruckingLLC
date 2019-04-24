using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Domain.Agents
{
    public interface IAgentQueue
    {
        void Add(Agent agent);
        Agent NextAgent();
        bool Contains(Agent agent);
        int SizeOf();
    }

}
