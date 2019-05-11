using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public class ServiceRequest : AggregateRoot<ServiceRequestDocument>
    {
        private Id _clientId;
        private Id _serviceId;
        private Id _assignedTo;
        
        private DueDate _dueDate;
        private ScheduledDate _scheduledDate;
        private CompletionDate _completionDate;

        private ServiceRequestState _state = ServiceRequestState.New;
        
        public ServiceRequest(ScheduledDate scheduledDate, 
            Id clientId, Id serviceId, Id assignedTo, 
            DueDate dueDate, Version version, Id id) : base(id, version)
        {
            this._scheduledDate = scheduledDate;
            this._clientId = clientId;
            this._serviceId = serviceId;
            this._dueDate = dueDate;
            this._assignedTo = assignedTo;
        }

        public ServiceRequest() : base(new Id(), new Version())
        {
        }

        public ServiceRequestState State
        {
            get { return _state; }
        }

        public Id IsAssignedTo()
        {
            return this._assignedTo;
        }

        public void Completed()
        {
            this._state = ServiceRequestState.Complete;
            this._completionDate = new CompletionDate(DateTime.Today);
            UpdateVersion();
        }

        public DueDate IsDue()
        {
            return this._dueDate;
        }

        public override void Load(ServiceRequestDocument document)
        {
            id = new Id(document.Id);
            version = new Version(document.Version);

            _scheduledDate = new ScheduledDate(document.ScheduledDate);
            _dueDate = new DueDate(document.DueDate);
            _completionDate = new CompletionDate(document.CompletionDate);

            _clientId = new Id(document.Client);
            _serviceId = new Id(document.Service);
            _assignedTo = new Id(document.AssignedAgent);

            _state = document.State;
           
        }

        public override ServiceRequestDocument ToDocument()
        {
            return new ServiceRequestDocument(
                _scheduledDate, 
                _clientId, 
                _serviceId, 
                _assignedTo, 
                _dueDate, 
                _state, 
                _completionDate
                ,version
                , Id);
        }

        private void UpdateVersion()
        {
            version++; 
        }
    }
}
