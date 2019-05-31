using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Commands.Tasks
{
    public class AssignTaskCommand : Command
    {
        public AssignTaskCommand(Guid assignedTo, DueDate dueOn, string task)
          : base(Guid.NewGuid())
        {
            AssignedTo = assignedTo;
            DueOn = dueOn;
            Task = task;
        }

        public Guid AssignedTo { get; private set; }
        public DueDate DueOn { get; private set; }
        public string Task { get; private set; }
    }
}
