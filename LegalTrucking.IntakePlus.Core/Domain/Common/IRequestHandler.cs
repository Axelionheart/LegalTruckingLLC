using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Domain.Common
{
    public interface IRequestHandler<TRequest> where TRequest : class
    {
        TRequest Handle(TRequest command);
    }
}
