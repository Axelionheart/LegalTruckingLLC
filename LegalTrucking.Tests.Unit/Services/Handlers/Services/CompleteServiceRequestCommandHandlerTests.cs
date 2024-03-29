﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using LegalTrucking.IntakePlus.Core.Adapters.Infrastructure.Notifications;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Services;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Services;
using LegalTrucking.IntakePlus.Core.Ports.Events.Services;
using LegalTrucking.IntakePlus.Core.Ports.Handlers.Services;
using LegalTrucking.IntakePlus.Core.Ports.ThinReadLayer;
using LegalTrucking.Tests.Unit.Fakes;
using Machine.Specifications;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.Tests.Unit.Services.Handlers.Services
{
    public class CompleteServiceRequestCommandHandlerTests
    {
        private static IRequestHandler<CompleteServiceRequestCommand> _completeServiceCommandHandler;
        private static CompleteServiceRequestCommand _completeServiceRequestCommand;

        private static IServiceRequestRepository _repository;
        private static INotifier _notifier;

        private static Guid requestId = Guid.NewGuid();
        private static Guid _clientId = Guid.NewGuid();
        private static Guid _serviceId = Guid.NewGuid();
        private static Guid _assignedTo = Guid.NewGuid();
        private static DueDate _due = new DueDate(DateTime.Now);
        private static ScheduledDate @on = new ScheduledDate(DateTime.Now);
        private static CompletionDate @completed = new CompletionDate(DateTime.Now);
        private static ServiceRequest _newServiceRequest;
        private static ServiceRequestCompletedEvent _event;

        Establish context = () =>
        {
            _repository = A.Fake<IServiceRequestRepository>();
            _newServiceRequest = new ServiceRequest(@on,
                new Id(_clientId),
                new Id(_serviceId),
                new Id(_assignedTo), 
                _due,
                new Version(),
                new Id(requestId));

            
             _notifier = A.Fake<INotifier>();


            A.CallTo(() => _repository.GetByIdAsync(A<Guid>.Ignored)).Returns(_newServiceRequest);


            _completeServiceRequestCommand = new CompleteServiceRequestCommand(requestId, 
                                                                                _assignedTo, 
                                                                                _assignedTo, 
                                                                                completed,
                                                                                0);
            _completeServiceCommandHandler = new CompleteServiceRequestCommandHandler(_repository, _notifier);

        };

        
        Because of = async () =>  _completeServiceRequestCommand = await _completeServiceCommandHandler.HandleAsync(command: _completeServiceRequestCommand);

     
        It should_ask_repository_for_servicerequest = () => A.CallTo(() => _repository.GetByIdAsync(A<Guid>.Ignored)).MustHaveHappened();
        It should_tell_the_servicerequest_it_is_complete = () => A.CallTo(() => _newServiceRequest.Completed()).MustHaveHappened();
        It should_change_the_status_to_complete = () => _newServiceRequest.State.ShouldBeTheSameAs(ServiceRequestState.Complete);
        It should_ask_repository_to_update_servicerequest = () => A.CallTo(() => _repository.UpdateAsync(A<ServiceRequest>.Ignored)).MustHaveHappened();
    }
}
