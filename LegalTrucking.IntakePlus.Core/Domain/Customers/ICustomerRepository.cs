using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Domain.Customers
{
    public interface ICustomerRepository : IRepository<Customer, CustomerDocument> {}
}
