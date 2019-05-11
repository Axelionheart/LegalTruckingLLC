using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Agents;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public class Scheduler: IScheduler
    {
        private IAgentQueue _queue;

        public Scheduler(IAgentQueue queue)
        {
            this._queue = queue;
        }

        public ServiceRequest Schedule(ScheduledDate dateScheduled, Id clientId, Id serviceId)
        {
            Agent next = AssignNextAvailableAgent();
            ServiceRequest request = new ServiceRequest(
                                            dateScheduled, 
                                            clientId, 
                                            serviceId, 
                                            next.Id, 
                                            new DueDate(DateTime.Now),
                                            new Version(), 
                                            new Id());
            return request;
        }

        private Agent AssignNextAvailableAgent()
        {
            return this._queue.NextAgent();
        }
    }
}
