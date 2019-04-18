﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Domain.Agents;

using Machine.Specifications;

namespace LegalTrucking.Tests.Unit.Domain.Agents
{
    [Subject(typeof(AgentQueue))]
    public class When_we_add_an_agent_they_get_added_to_the_queue
    {
        static AgentQueue _agentQueue;
        static Agent agent = new Agent("Adrian", "Tillman");
        static bool doesContain = false;

        Establish context = () =>
        {
            _agentQueue = new AgentQueue(1);
        };

        Because of = () => _agentQueue.AddAn(agent);

        It should_contain_that_agent = () => _agentQueue.Contains(agent).ShouldEqual(true);
    }

    [Subject(typeof(AgentQueue))]
    public class When_we_add_an_agent_if_the_queue_is_full_it_grows{
        static AgentQueue agentQueue;
        static Agent adrian = new Agent("Adrian", "Tillman");
        static Agent fred = new Agent("Fred", "Sandford");

        Establish context = () =>
        {
            agentQueue = new AgentQueue(1);
            agentQueue.AddAn(adrian);
        };

        Because of = () => agentQueue.AddAn(fred);

        It should_have_increased_the_capacity_to_be_greater_than_original = () => agentQueue.SizeOf().ShouldEqual(2);

    }
}
