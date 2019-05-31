using FakeItEasy;
using Machine.Specifications;
using System;


using LegalTrucking.IntakePlus.Core.Domain.Customers;
using LegalTrucking.IntakePlus.Core.Ports.Handlers.Customers;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Customers;
using LegalTrucking.IntakePlus.Core.Domain.Common;

namespace LegalTrucking.Tests.Unit.Services.Handlers.Customers
{
    [Subject(typeof(AddCustomerCommandHandler))]
    public class WhenAAddCustomerCommandIsReceived
    {
        private static ICustomerRepository _repository;
        private static IRequestHandler<AddCustomerCommand> _requestHandler;
        private static AddCustomerCommand _request;

        private static readonly string _customerName = "Anderson Trucking";
        private static readonly Street _street = new Street("3049 35th Ave. S.");
        private static readonly City _city = new City("St Petersburg");
        private static readonly PostCode _postalCode = new PostCode("33712");

        private static readonly Address _billingAddress = new Address(_street,_city, _postalCode);
        private static readonly Address _shippingAddress = new Address(_street, _city, _postalCode);

        private static readonly Name _contactName = new Name("Adrian Tillman");
        private static readonly PhoneNumber _phoneNumber = new PhoneNumber("813412,0948");
        private static readonly EmailAddress _customerEmail = new EmailAddress("adrian.tillman@gmail.com");
        private static readonly Contact _contactPerson = new Contact(_contactName, _customerEmail, _phoneNumber);

        Establish context = () =>
        {
            _repository = A.Fake<ICustomerRepository>();
            
            _request = new AddCustomerCommand(_customerName, 
                                                _billingAddress,
                                                _shippingAddress,
                                                _contactPerson);

            
            _requestHandler = new AddCustomerCommandHandler(_repository);

        };

        Because of = () => _requestHandler.HandleAsync(command: _request);

        It should_add_a_customer_to_the_repository = () => A.CallTo(() => _repository.AddAsync(A<Customer>.Ignored)).MustHaveHappened();
        
    }
}
