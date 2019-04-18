using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Services;

namespace LegalTrucking.IntakePlus.Core.Ports.Commands.Services
{
    public class ServiceRequestCommand : Command
    {
       
        public ServiceRequestCommand(Guid clientId, Guid serviceId, DateTime requestedOn, IFormData data) : base(Guid.NewGuid())
        {
            ClientId = clientId;
            ServiceId = serviceId;
            RequestedOn = requestedOn;
            FormData = data;
        }
        
        public Guid ClientId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime RequestedOn { get; set; }
        public IFormData FormData { get; set; }
    }
}
