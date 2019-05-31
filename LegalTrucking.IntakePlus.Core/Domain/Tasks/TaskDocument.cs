using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Domain.Tasks
{
    public class TaskDocument : IAmADocument
    {
        private Id id;
        private object verson;
        private Id _assignedTo;
        private DueDate _dueOn;
        private string _task;

        public TaskDocument(Id id, object verson, Id assignedTo, DueDate dueOn, string task)
        {
            this.id = id;
            this.verson = verson;
            _assignedTo = assignedTo;
            _dueOn = dueOn;
            _task = task;
        }
    }
}
