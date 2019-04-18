using LegalTrucking.IntakePlus.Core.Domain.Common.CircularBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Domain.Agents
{
    public class AgentQueue
    {
        private CircularBuffer<Agent> _buffer;

        public AgentQueue()
        {
            _buffer = new CircularBuffer<Agent>(10);
        }

        public void AddAn(Agent agent)
        {
            _buffer.PushBack(agent);
        }

        public bool Contains(Agent agent)
        {
            return _buffer.Contains(agent);
        }
    }
}
