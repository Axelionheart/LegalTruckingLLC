using FakeItEasy;
using LegalTrucking.IntakePlus.Core.Domain.Agents;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Services;
using LegalTrucking.IntakePlus.Core.Domain.Tasks;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Tasks;
using LegalTrucking.IntakePlus.Core.Ports.Handlers.Tasks;
using Machine.Specifications;
using System;

namespace LegalTrucking.Tests.Unit.Services.Handlers.Tasks
{

    [Subject(typeof(AssignTaskCommandHandler))]
    public class WhenAAssignTaskCommandIsReceived
    {
        private static ITaskRepository _repository;
        private static IRequestHandler<AssignTaskCommand> _requestHandler;
        private static AssignTaskCommand _request;
        private static DueDate dueDate = new DueDate(DateTime.Now);

        private static Agent agent = new Agent("Adrian", "Tillman");
        
        Establish context = () =>
        {
            _repository = A.Fake<ITaskRepository>();

            _request = new AssignTaskCommand(agent.Id, dueDate, "COMPLETE 2290 FOR SCOTT SAVAGE");


            _requestHandler = new AssignTaskCommandHandler(_repository);

        };

        Because of = () => _requestHandler.HandleAsync(command: _request);

        It should_add_a_task_to_the_repository = () => A.CallTo(() => _repository.AddAsync(A<Task>.Ignored)).MustHaveHappened();

    }
}
