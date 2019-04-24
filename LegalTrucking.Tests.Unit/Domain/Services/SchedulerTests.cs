using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Agents;
using LegalTrucking.IntakePlus.Core.Domain.Services;

using Machine.Specifications;

namespace LegalTrucking.Tests.Unit.Domain.Services
{
    [Subject(typeof(Scheduler))]
    public class When_we_schedule_a_service_it_gets_assigned_an_agent
    {
        static Scheduler scheduler;
        static ServiceRequest request;
        static ScheduledDate @on;
        static Id clientId;
        static Id serviceId;
        static IAgentQueue agentQueue;
        static Agent adrian = new Agent("Adrian", "Tillman");
        static Guid requestId;

        private Establish context = () =>
        {
            agentQueue = A.Fake<IAgentQueue>();
            A.CallTo(() => agentQueue.NextAgent()).Returns(adrian);

            scheduler = new Scheduler(agentQueue);
            @on = new ScheduledDate(DateTime.Now);
            clientId = new Id(Guid.NewGuid());
            serviceId = new Id(Guid.NewGuid());
            requestId = new Id(Guid.NewGuid());
        };

        Because of = () => request = scheduler.Schedule(@on, clientId, serviceId);

        It should_have_an_agent_assigned = () => ((Guid)request.IsAssignedTo()).ShouldEqual((Guid)adrian.Id);
        It should_have_a_new_request_id = () => request.Id.ShouldBeOfExactType<Id>();
        It should_have_a_status_of_new = () => request.State.ShouldEqual(ServiceRequestState.New);
        It should_have_a_due_date_different_than_request_date = () => request.IsDue().Value.ShouldBeGreaterThan(@on.Value);
    }
}
