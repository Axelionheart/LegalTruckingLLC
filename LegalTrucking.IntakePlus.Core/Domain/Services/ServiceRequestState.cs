using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public enum ServiceRequestState
    {
        New, 
        InProgress,
        OnHold,
        Complete
    }
}
