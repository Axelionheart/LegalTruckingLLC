using LegalTrucking.IntakePlus.Core.Domain.Common;

using System;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Domain.Customers;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Customers;

namespace LegalTrucking.IntakePlus.Core.Ports.Handlers.Customers
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand>
    {
        private ICustomerRepository _repository;
        
        public AddCustomerCommandHandler(ICustomerRepository repository)
        {
            this._repository = repository;
        }
        public async Task<AddCustomerCommand> HandleAsync(AddCustomerCommand command)
        {
            var customer = new Customer(
                command.Name, command.BillingAddress,
                command.ShippingAddress, command.Contact);

            customer = await _repository.AddAsync(customer);

            command.Id = customer.Id;

            return command;
        }
    }
}
