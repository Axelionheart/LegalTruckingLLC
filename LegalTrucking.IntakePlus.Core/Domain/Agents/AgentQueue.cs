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

        public AgentQueue(int capacity)
        {
            _buffer = new CircularBuffer<Agent>(capacity);
        }

        public void Add(Agent agent)
        {
            if (this.SizeOf() == _buffer.Capacity)
                IncreaseBufferCapacity();

            _buffer.PushBack(agent);
        }

        public Agent NextAgent()
        {
            var next = this._buffer.Front();
            this._buffer.PopFront();
            this._buffer.PushBack(next);
            return next;
        }

        public bool Contains(Agent agent)
        {
            return _buffer.Contains(agent);
        }

        public int SizeOf()
        {
            return this._buffer.Size;
        }

        private void IncreaseBufferCapacity()
        {
            var newBuffer = new CircularBuffer<Agent>(_buffer.Size + 1);
            Copy(_buffer, newBuffer);
            this._buffer = newBuffer;
        }

        private void Copy(CircularBuffer<Agent> from, CircularBuffer<Agent> to)
        {
            foreach(var agent in from)
            {
                to.PushBack(agent);
            }
        }

        
    }
}
