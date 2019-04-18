using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Adapters.Repositories;

namespace LegalTrucking.IntakePlus.Core.Domain.Common
{
    public abstract class AggregateRoot : IAmAnAggregateRoot
    {
        protected Id id;

        protected AggregateRoot(Guid id)
        {
            this.id = new Id(id);
        }

        protected AggregateRoot(Id id)
        {
            this.id = id;
        }

        public Id Id
        {
            get { return id; }
        }

    }
}
