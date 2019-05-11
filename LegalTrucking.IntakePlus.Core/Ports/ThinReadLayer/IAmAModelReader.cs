using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;

namespace LegalTrucking.IntakePlus.Core.Ports.ThinReadLayer
{
    public interface IAmAViewModelReader<out TDocument> where TDocument : IAmADocument
    {
        IEnumerable<TDocument> GetAll();
        TDocument Get(Guid id);
    }
}
