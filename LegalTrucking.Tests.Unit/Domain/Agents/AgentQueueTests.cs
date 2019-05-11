using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Agents;

using Machine.Specifications;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.Tests.Unit.Domain.Agents
{
    [Subject(typeof(AgentQueue))]
    public class When_we_add_an_agent_they_get_added_to_the_queue
    {
        static AgentQueue _agentQueue;
        static Agent agent = new Agent("Adrian", 
                                        "Tillman",
                                        new Version(),
                                        new Id());
        static bool doesContain = false;

        Establish context = () =>
        {
            _agentQueue = new AgentQueue(1);
        };

        Because of = () => _agentQueue.Add(agent);

        It should_contain_that_agent = () => _agentQueue.Contains(agent).ShouldEqual(true);
    }

    [Subject(typeof(AgentQueue))]
    public class When_we_add_an_agent_if_the_queue_is_full_it_grows{
        static AgentQueue agentQueue;
        static Agent adrian = new Agent("Adrian", "Tillman", new Version(), new Id() );
        static Agent fred = new Agent("Fred", "Sandford", new Version(), new Id());

        Establish context = () =>
        {
            agentQueue = new AgentQueue(1);
            agentQueue.Add(adrian);
        };

        Because of = () => agentQueue.Add(fred);

        It should_have_increased_the_capacity_to_be_greater_than_original = () => agentQueue.SizeOf().ShouldEqual(2);

    }

    [Subject(typeof(AgentQueue))]
    public class When_we_get_an_agent_they_move_to_the_back_of_queue
    {
        static AgentQueue agentQueue;
        static Agent adrian = new Agent("Adrian", "Tillman", new Version(),new Id());
        static Agent fred = new Agent("Fred", "Sanford", new Version(), new Id());
        static Agent nextUp;
        static Agent shouldBeAdrian;
        static Agent shouldbeFred;

        Establish context = () =>
        {
            agentQueue = new AgentQueue(1);
            agentQueue.Add(adrian);
            agentQueue.Add(fred);
            shouldBeAdrian = agentQueue.NextAgent();
            shouldbeFred = agentQueue.NextAgent();
        };

        Because of = () => nextUp = agentQueue.NextAgent();

        It should_get_adrian_first = () => shouldBeAdrian.ShouldEqual(adrian);
        It should_get_fred_next = () => shouldbeFred.ShouldEqual(fred);
        It should_be_adrian_again = () => nextUp.ShouldEqual(adrian);

    }
}
