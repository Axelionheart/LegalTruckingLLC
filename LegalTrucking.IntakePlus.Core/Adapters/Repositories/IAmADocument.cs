using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Adapters.Repositories
{
    public interface IAmADocument
    {
        //AzureDB needs properties to store documents and public properties for LINQ queries
        //it can also be useful to check state
        //so we can ask an aggregate for its IAmADocment, which we then store from AzureDB
    }

    public interface IAmPartOfADocument
    {
        //Aggregates are a documents, entities are parts of documents, so we treat the entity as different from
        //a document
    }
}
