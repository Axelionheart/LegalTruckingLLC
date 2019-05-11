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
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.Tests.Unit.Services.Handlers.Services
{

    [Subject(typeof(RequestServiceCommandHandler))]
    public class WhenARequestServiceCommandIsReceived
    {
        private static IRequestHandler<ServiceRequestCommand> _requestServiceCommandHandler;
        private static ServiceRequestCommand _newServiceRequest;
        private static IRepository<ServiceRequest, ServiceRequestDocument> _repository;
        private static IScheduler _scheduler;


        private static readonly Guid _clientId = Guid.NewGuid();
        private static readonly DateTime @on = DateTime.Today;
        private static readonly Guid _serviceId = Guid.NewGuid();
        private static readonly Guid _assignedTo = Guid.NewGuid();
        private static IFormData _formData;

        Establish context = () =>
        {
            _repository = A.Fake<IRepository<ServiceRequest, ServiceRequestDocument>>();
            _scheduler = A.Fake<IScheduler>();
          

            _newServiceRequest = new ServiceRequestCommand(_clientId, _serviceId, @on, _formData);
                
            A.CallTo(() => _scheduler.Schedule(new ScheduledDate(@on), new Id(_clientId), new Id(_serviceId)))
                .Returns(new ServiceRequest(new ScheduledDate(@on), 
                                            new Id(_clientId), 
                                            new Id(_assignedTo), 
                                            new Id(_serviceId), 
                                            new DueDate(on.AddDays(30)),
                                            new Version(),
                                            new Id()));

         
            _requestServiceCommandHandler = new RequestServiceCommandHandler(_scheduler, _repository);

        };

        Because of = () => _requestServiceCommandHandler.HandleAsync(command: _newServiceRequest);

        It should_add_a_service_request_to_the_repository = () => A.CallTo(() => _repository.AddAsync(A<ServiceRequest>.Ignored)).MustHaveHappened();
        It should_ask_the_scheduler_to_create_an_instance_of_a_ServiceRequest = () => A.CallTo(() => _scheduler.Schedule(new ScheduledDate(@on), new Id(_clientId), new Id(_serviceId))).MustHaveHappened();
       
    }
    

}
