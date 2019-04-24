using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Services;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Services;
using LegalTrucking.IntakePlus.Core.Ports.Handlers.Services;

using Machine.Specifications;
using Xunit;
using FakeItEasy;

namespace LegalTrucking.Tests.Unit.Services.Handlers.Services
{

    [Subject(typeof(RequestServiceCommandHandler))]
    public class WhenARequestServiceCommandIsReceived
    {
        private static IRequestHandler<ServiceRequestCommand> _requestServiceCommandHandler;
        private static ServiceRequestCommand _newServiceRequest;
        private static IRepository<ServiceRequest> _repository;
        private static IScheduler _scheduler;
        private static IAmAUnitOfWorkFactory _uoWFactory;
        private static IUnitOfWork _uow;

        private static readonly Guid _clientId = Guid.NewGuid();
        private static readonly DateTime @on = DateTime.Today;
        private static readonly Guid _serviceId = Guid.NewGuid();
        private static readonly Guid _assignedTo = Guid.NewGuid();
        private static IFormData _formData;

        Establish context = () =>
        {
            _repository = A.Fake<IRepository<ServiceRequest>>();
            _scheduler = A.Fake<IScheduler>();
            _uoWFactory = A.Fake<IAmAUnitOfWorkFactory>();
            _uow = A.Fake<IUnitOfWork>();

            _newServiceRequest = new ServiceRequestCommand(_clientId, _serviceId, @on, _formData);
                
            A.CallTo(() => _scheduler.Schedule(new ScheduledDate(@on), new Id(_clientId), new Id(_serviceId)))
                .Returns(new ServiceRequest(new ScheduledDate(@on), 
                                            new Id(_clientId), 
                                            new Id(_assignedTo), 
                                            new Id(_serviceId), 
                                            new DueDate(on.AddDays(30))));

            A.CallTo(() => _uoWFactory.CreateUnitOfWork()).Returns(_uow);
            _requestServiceCommandHandler = new RequestServiceCommandHandler(_scheduler, _repository, _uoWFactory);

        };

        Because of = () => _requestServiceCommandHandler.Handle(command: _newServiceRequest);

        It should_add_a_service_request_to_the_repository = () => A.CallTo(() => _repository.Add(A<ServiceRequest>.Ignored)).MustHaveHappened();
        It should_ask_the_factory_to_create_an_instance_of_a_ServiceRequest = () => A.CallTo(() => _scheduler.Schedule(new ScheduledDate(@on), new Id(_clientId), new Id(_serviceId))).MustHaveHappened();
        It should_ask_the_session_factory_for_a_unit_of_work = () => A.CallTo(() => _uoWFactory.CreateUnitOfWork()).MustHaveHappened();
        It should_commit_the_unit_of_work = () => A.CallTo(() => _uow.Commit()).MustHaveHappened();

    }
    

}
