using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using Newtonsoft.Json;
using System;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Customers
{
    public class CustomerDocument : IAmADocument
    {
        public CustomerDocument(Id id, Version version, string name, Address billingAddress,
           Address shippingAddress, Contact contact)
        {
            Id = id;
            Version = version;
            Name = name;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            Contact = contact;
        }

        public CustomerDocument() { }


        [JsonProperty("id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string Contact { get; set; }
        public int Version { get; set; }
    }
}