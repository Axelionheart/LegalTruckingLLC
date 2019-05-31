using System;

using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Services;
using Version = LegalTrucking.IntakePlus.Core.Adapters.Repositories.Version;

namespace LegalTrucking.IntakePlus.Core.Domain.Tasks
{
    public class Task : AggregateRoot<TaskDocument>
    {
        private Id _assignedTo;
        private DueDate _dueOn;
        private CompletionDate _completedOn;
        private string _task;
        private TaskStatus _status = TaskStatus.New;

        public Task(Id agent, DueDate dueOn, string task) : this(agent, dueOn, task, new Version(), new Id()) { }

        public Task(Id agent, DueDate dueOn, string task, Version version, Id id) : base(id, version)
        {
            this._assignedTo = agent;
            this._dueOn = dueOn;
            this._task = task;
        }

        public override void Load(TaskDocument document)
        {
            throw new NotImplementedException();
        }

        public override TaskDocument ToDocument()
        {
            return new TaskDocument(id, version, _assignedTo, _dueOn, _task);
        }
    }
}
