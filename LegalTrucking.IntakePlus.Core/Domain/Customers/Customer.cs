using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;

namespace LegalTrucking.IntakePlus.Core.Domain.Customers
{
    public class Customer : AggregateRoot<CustomerDocument>
    {
        private string _name;
        private Address _billingAddress;
        private Address _shippingAddress;
        private Contact _contactInfo;

        public Customer(string name, Address billingAddress,
            Address shippingAddress,Contact contact) :
            this(name, billingAddress, shippingAddress, contact, new Version(), new Id())
        {

        }

        public Customer(string name, Address billingAddress, 
            Address shippingAddress, Contact contact, Version version, Id id) : base(id, version)
        {
            _name = name;
            _billingAddress = billingAddress;
            _shippingAddress = shippingAddress;
            _contactInfo = contact;
        }

        public Customer() : base(new Id(), new Version()) { }

        public override void Load(CustomerDocument document)
        {
            _name = document.Name;
            _billingAddress = Address.Parse(document.BillingAddress);
            _shippingAddress = Address.Parse(document.ShippingAddress);
            _contactInfo = Contact.Parse(document.Contact);
        }

        public override CustomerDocument ToDocument()
        {
            return new CustomerDocument(id, version, _name, _billingAddress, _shippingAddress, _contactInfo);
        }

        public override string ToString()
        {
            return string.Format("Customer: {0}, Contact: {1}, Billing Address: {2}, Shipping Address: {3}", _name, _contactInfo, _billingAddress, _shippingAddress);
        }

    }
}