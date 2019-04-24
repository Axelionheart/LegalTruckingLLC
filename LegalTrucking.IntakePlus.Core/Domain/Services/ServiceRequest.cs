using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Agents;

namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public class ServiceRequest : AggregateRoot
    {
        private readonly Id _clientId;
        private Id _serviceId;
        private Id _assignedTo;
        private DueDate _dueDate;
        private ScheduledDate scheduledDate;
        private ServiceRequestState _state = ServiceRequestState.New;
        
        public ServiceRequest(ScheduledDate scheduledDate, 
            Id clientId, Id serviceId, Id assignedTo, 
            DueDate dueDate): base(Guid.NewGuid())
        {
            this.scheduledDate = scheduledDate;
            this._clientId = clientId;
            this._serviceId = serviceId;
            this.scheduledDate = scheduledDate;
            this._dueDate = dueDate;
            this._assignedTo = assignedTo;
        }

        public ServiceRequestState State
        {
            get { return _state; }
        }

        public Id IsAssignedTo()
        {
            return this._assignedTo;
        }

        public DueDate IsDue()
        {
            return this._dueDate;
        }
    }
}
