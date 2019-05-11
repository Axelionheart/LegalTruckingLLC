using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;

namespace LegalTrucking.Tests.Unit.Fakes
{
    internal class FakeRepository<T, TDocument> : IRepository<T, TDocument>
        where T : IAmAnAggregateRoot<TDocument>, new()
        where TDocument : IAmADocument
    {
        readonly List<T> members = new List<T>();

        #region IRepository<T,TDocument> Members

        public void Add(T aggregate)
        {
            members.Add(aggregate);
        }

        public T this[System.Guid id]
        {
            get
            {
                return members.SingleOrDefault(member => member.Id == id);
            }
        }


        public void Delete(T aggregate)
        {
            members.RemoveAll(member => member.Id == aggregate.Id);
        }

        public async Task<T> GetByIdAsync(Guid id) => members.SingleOrDefault(member => member.Id == id);

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}
