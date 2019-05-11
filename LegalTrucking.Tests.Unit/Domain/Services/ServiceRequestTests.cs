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
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.Tests.Unit.Domain.Services
{
    [Subject(typeof(ServiceRequest))]
    public class When_We_Complete_A_ServiceRequest
    {
        static ServiceRequest request;
        static Guid id = Guid.NewGuid();
        static ScheduledDate @on;
        static Id clientId;
        static Id serviceId;
        static IAgentQueue agentQueue;
        static Agent adrian = new Agent("Adrian", "Tillman", new Version(), new Id(id));
        static Guid requestId;
        static CompletionDate completedDate = new CompletionDate(DateTime.Now);

        Establish context = () =>
        {
            @on = new ScheduledDate(DateTime.Now);
            clientId = new Id(Guid.NewGuid());
            serviceId = new Id(Guid.NewGuid());
            requestId = new Id(Guid.NewGuid());
            request = new ServiceRequest(@on, 
                clientId, 
                serviceId, 
                adrian.Id, 
                new DueDate(DateTime.Now),
                new Version(),
                new Id(id));
        };

        Because of = () => request.Completed();

        It should_have_a_status_of_complete = () => request.State.ShouldEqual(ServiceRequestState.Complete);
    }
}
