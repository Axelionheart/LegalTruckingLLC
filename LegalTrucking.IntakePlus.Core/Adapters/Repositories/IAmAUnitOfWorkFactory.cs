using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Adapters.Repositories
{
    public interface IAmAUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
