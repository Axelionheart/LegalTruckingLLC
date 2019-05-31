using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Commands.Customers
{
    public class AddCustomerCommand : Command
    {
        public AddCustomerCommand(String customerName, Address billing, Address shipping, 
            Contact contact)
           : base(Guid.NewGuid())
        {
            Name = customerName;
            BillingAddress = billing;
            ShippingAddress = shipping;
            Contact = contact;
        }
     
        public string Name { get; internal set; }
        public Address BillingAddress { get; internal set; }
        public Address ShippingAddress { get; internal set; }
        public Contact Contact { get; internal set; }
    }
}
