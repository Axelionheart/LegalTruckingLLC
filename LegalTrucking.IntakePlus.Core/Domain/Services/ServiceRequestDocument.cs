using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public class ServiceRequestDocument : IAmADocument
    {

        public ServiceRequestDocument(ScheduledDate scheduledDate,
            Id clientId, Id serviceId, Id assignedTo,
            DueDate dueDate, ServiceRequestState state, 
            CompletionDate completed,
            Version version, Id id)
        {
            this.ScheduledDate = scheduledDate ?? new ScheduledDate(DateTime.Now);
            this.Client = clientId;
            this.Service = serviceId;
            this.AssignedAgent = assignedTo ?? Guid.Empty;
            this.DueDate = dueDate ?? new DueDate(DateTime.Now.AddDays(2));
            this.CompletionDate = completed;
            this.State = state;
            this.Version = version;
            this.Id = id;
        }

        public ServiceRequestDocument() { }

        public ScheduledDate ScheduledDate { get; set; }
        public Guid Client { get; set; }
        public Guid  Service { get; set; }
        public Guid AssignedAgent { get; set; }
        public DueDate DueDate { get; set; }
        public ServiceRequestState State { get; set; }
        public CompletionDate CompletionDate { get; set; }
        public Version Version { get; set; }
        public Guid Id { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, ScheduledDate: {1}, Client: {2}, Service: {3}, Agent: {4}, DueDate: {5}, State {6}, Version: {7}", Id, 
                ScheduledDate, Client, Service,
                AssignedAgent, DueDate, State, Version);
        }
    }
}
