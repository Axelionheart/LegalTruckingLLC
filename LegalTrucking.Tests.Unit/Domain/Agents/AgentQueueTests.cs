using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Domain.Agents;

using Machine.Specifications;

namespace LegalTrucking.Tests.Unit.Domain.Agents
{
    public class When_we_add_an_agent_they_get_added_to_the_queue
    {
        static AgentQueue _agentQueue;
        static Agent agent = new Agent("Adrian", "Tillman");
        static bool doesContain = false;

        Establish context = () =>
        {
            _agentQueue = new AgentQueue();
        };

        Because of = () => _agentQueue.AddAn(agent);

        It should_contain_that_agent = () => _agentQueue.Contains(agent).ShouldEqual(true);
    }
}
