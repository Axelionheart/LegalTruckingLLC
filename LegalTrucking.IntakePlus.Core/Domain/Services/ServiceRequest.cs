using LegalTrucking.IntakePlus.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Adapters.Repositories;

namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public class ServiceRequest : AggregateRoot
    {
        private Id _clientId;
        private Id _serviceId;
        private Id _assignedTo;
        private DueDate dueDate;
        private ScheduledDate scheduledDate;
      
        
        public ServiceRequest(ScheduledDate scheduledDate, Id clientId, Id serviceId, Id assignedTo, DueDate dueDate): base(Guid.NewGuid())
        {
            this.scheduledDate = scheduledDate;
            this._clientId = clientId;
            this._serviceId = serviceId;
            this.scheduledDate = scheduledDate;
            this.dueDate = dueDate;
        }

    }
}
