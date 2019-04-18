using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Commands.Services
{
    public class ServiceRequestCommand
    {
        public ServiceRequestCommand(Guid id) : base(id)
        {
        }

        public ServiceRequestCommand(Guid id, DateTime on, Guid location, Guid speaker, int capacity) : base(id)
        {
            On = on;
            VenueId = location;
            SpeakerId = speaker;
            Capacity = capacity;
            MeetingId = id;
        }

        public ServiceRequestCommand() : base(Guid.NewGuid())
        {
        }

    }
}
