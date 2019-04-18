using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Adapters.Repositories;

namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public interface IScheduler
    {
        ServiceRequest Schedule(ScheduledDate dateScheduled, Id clientId, Id serviceId);
    }
}
